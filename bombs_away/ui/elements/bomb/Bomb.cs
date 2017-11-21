using bombs_away.ui.enums;
using bombs_away.ui.physics;
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
        float timeDeltaToExplode = 3;
        
        private BombState state = BombState.IDLE;
        public BombState State { get { return state; } }

        public override void Execute(float updatePeriod)
        {
            timeDeltaToExplode -= updatePeriod;
            if (timeDeltaToExplode < 0)
            {
                Console.WriteLine(timeDeltaToExplode);
                explode(updatePeriod);
            }
            base.Execute(updatePeriod);
        }

        public virtual void explode(float updatePeriod)
        {
            Console.WriteLine("Explode");
            component.SizeX += component.SizeX * 7.5f * updatePeriod;
            component.MinX -= updatePeriod * 2;
            state = BombState.EXPLODE;
            //expand rectangle
        }
    }
}
