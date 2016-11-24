using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace wServer.realm
{
	public class LogicTicker
	{
		/// <summary>
		/// Returns the task scheduler who will perform tasks on the logic thread
		/// Note: Use this task scheduler only for sync operations, not for blocking operations.
		/// </summary>
		public static TaskScheduler TaskScheduler { get; } = new LogicThreadTaskScheduler();

		private static readonly ILog Log = LogManager.GetLogger(nameof(LogicTicker));

		private readonly RealmManager manager;
		private readonly ConcurrentQueue<Action<RealmTime>>[] pendings;

		public readonly int TPS;
		public readonly int MsPT;

		private readonly ManualResetEvent mre;
		private Task worldTask;
		private RealmTime worldTime;

		public LogicTicker(RealmManager manager)
		{
			this.manager = manager;
			MsPT = 1000 / manager.TPS;
			mre = new ManualResetEvent(false);
			worldTime = new RealmTime();

			pendings = new ConcurrentQueue<Action<RealmTime>>[5];
			for (int i = 0; i < 5; i++)
				pendings[i] = new ConcurrentQueue<Action<RealmTime>>();
		}

		public void TickLoop()
		{
			Log.Info("Logic loop started.");

			var loopTime = 0;
			var t = new RealmTime();
			var watch = Stopwatch.StartNew();
			do
			{
				t.TotalElapsedMs = watch.ElapsedMilliseconds;
				t.TickDelta = loopTime / MsPT;
				t.TickCount += t.TickDelta;
				t.ElaspedMsDelta = t.TickDelta * MsPT;

				if (t.TickDelta > 3)
					Log.Warn("LAGGED! | ticks:" + t.TickDelta +
									  " ms: " + loopTime +
									  " tps: " + t.TickCount / (t.TotalElapsedMs / 1000.0));

				if (manager.Terminating)
					break;

				DoLogic(t);

				var logicTime = (int)(watch.ElapsedMilliseconds - t.TotalElapsedMs);
				mre.WaitOne(Math.Max(0, MsPT - logicTime));
				loopTime += (int)(watch.ElapsedMilliseconds - t.TotalElapsedMs) - t.ElaspedMsDelta;
			} while (true);
			Log.Info("Logic loop stopped.");
		}

		private void DoLogic(RealmTime t)
		{
			var clients = manager.Clients.Values;

			foreach (var i in pendings)
			{
				Action<RealmTime> callback;
				while (i.TryDequeue(out callback))
					try
					{
						callback(t);
					}
					catch (Exception e)
					{
						Log.Error(e);
					}
			}

			(TaskScheduler as LogicThreadTaskScheduler)?.RunPendingTasks();

			TickWorlds1(t);

			foreach (var client in clients.Where(client => client.Player?.Owner != null))
				client.Player.Flush();
		}

		void TickWorlds1(RealmTime t)    //Continous simulation
		{
			worldTime.TickDelta += t.TickDelta;

			// tick enemies
			try
			{
				foreach (var w in manager.Worlds.Values.Distinct())
					w.TickLogic(t);
			}
			catch (Exception e)
			{
				Log.Error(e);
			}

			// tick world every 200 ms
			if (worldTask != null && !worldTask.IsCompleted) return;
			t.TickDelta = worldTime.TickDelta;
			t.ElaspedMsDelta = t.TickDelta * MsPT;

			if (t.ElaspedMsDelta < 200)
				return;

			worldTime.TickDelta = 0;
			worldTask = Task.Factory.StartNew(() =>
			{
				foreach (var i in manager.Worlds.Values.Distinct())
					i.Tick(t);
			}).ContinueWith(e =>
				Log.Error(e.Exception.InnerException.ToString()),
				TaskContinuationOptions.OnlyOnFaulted);
		}

		public void AddPendingAction(Action<RealmTime> callback,
			PendingPriority priority = PendingPriority.Normal)
		{
			pendings[(int)priority].Enqueue(callback);
		}

		private class LogicThreadTaskScheduler : TaskScheduler
		{
			[ThreadStatic]
			private static bool isExecuting;

			private readonly BlockingCollection<Task> taskQueue;

			public LogicThreadTaskScheduler()
			{
				taskQueue = new BlockingCollection<Task>();
			}

			private void internalRunOnCurrentThread()
			{
				isExecuting = true;

				try
				{
					if (taskQueue.Count == 0) return;
					foreach (var task in taskQueue.GetConsumingEnumerable())
					{
						TryExecuteTask(task);
					}
				}
				catch (OperationCanceledException)
				{ }
				finally
				{
					isExecuting = false;
				}
			}

			public void Complete() { taskQueue.CompleteAdding(); }
			protected override IEnumerable<Task> GetScheduledTasks() { return null; }

			protected override void QueueTask(Task task)
			{
				try
				{
					taskQueue.Add(task);
				}
				catch (OperationCanceledException) { }
			}

			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (taskWasPreviouslyQueued) return false;
				return isExecuting && TryExecuteTask(task);
			}

			public void RunPendingTasks()
			{
				if (Thread.CurrentThread.Name != "Logic")
					throw new InvalidOperationException("Method can only be called from the logic thread.");
				internalRunOnCurrentThread();
			}
		}
	}
}