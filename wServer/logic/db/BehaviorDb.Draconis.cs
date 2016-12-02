using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.logic.behaviors;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ LairOfDraconis = () => Behav()
            .Init("NM Black Dragon God",
                new State(
				      new ThrowProjectile(0x7565, 4, 0, 5000),
				      new ThrowProjectile(0x7564, 4, 90, 5000),
				      new ThrowProjectile(0x7563, 4, 180, 5000),
				      new ThrowProjectile(0x7562, 4, 270, 5000)
                )
            );
    }
}
