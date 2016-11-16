using db;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace wServer.realm
{
	public class DatabaseTicker
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseTicker));

		public DatabaseTicker(RealmManager manager)
		{
			Manager = manager;
		}

		public RealmManager Manager { get; }

		public Task AddDatabaseOperation(Action<Database> action, Action<Exception> onException = null, bool logException = true)
		{
			return Task.Factory.StartNew(() =>
			{
				using (var db = new Database())
					action(db);
			}).ContinueWith(t =>
			{
				var exception = t.Exception.InnerException;
				onException?.Invoke(exception);
				if (logException)
					log.Error("Error in database task:", exception);
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		/**public Task<TResult> AddDatabaseOperation<TResult>(Func<Database, TResult> func, Action<Exception> onException = null, bool logException = true)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var db = new Database())
                    return func(db);
            });
            ContinueWith<TResult>(t =>
            {
                var exception = t.Exception.InnerException;
                onException?.Invoke(exception);
                if (logException)
                    log.Error("Error in database task:", exception);
                return t.Result;
            }, TaskContinuationOptions.OnlyOnFaulted);
        } **/
	}
}