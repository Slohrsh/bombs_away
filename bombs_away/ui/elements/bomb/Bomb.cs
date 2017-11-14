using bombs_away.ui.enums;
using bombs_away.ui.physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.elements.bomb
{
    abstract class Bomb : PhysicsObject
    {
        float timeDeltaToExplode = 3;
        
        private BombState state = BombState.IDLE;
        public BombState State { get; }

        public virtual void explode()
        {


            state = BombState.EXPLODE;
            //expand rectangle
        }
    }
}
