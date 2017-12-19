using bombs_away.ui.enums;
using bombs_away.ui.physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.bomb
{
    abstract class Bomb : PhysicsObject
    {
        private BombState state = BombState.IDLE;
        public BombState State { get { return state; } }

        protected float timeDeltaToExplode = 1;

        public override Vector2 Execute(float updatePeriod)
        {
            timeDeltaToExplode -= updatePeriod;
            if (timeDeltaToExplode < 0)
            {
                Explode(updatePeriod);
            }
            return base.Execute(updatePeriod);
        }

        public virtual void Explode(float updatePeriod)
        {
            Console.WriteLine("Explode");
            state = BombState.EXPLODE;
        }
    }
}
