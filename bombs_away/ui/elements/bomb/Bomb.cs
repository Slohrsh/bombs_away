using bombs_away.ui.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.elements.bomb
{
    class Bomb : Drawable
    {
        private BombState state = BombState.IDLE;
        public BombState State { get; }
        public void explode()
        {
            state = BombState.EXPLODE;
            //expand rectangle
        }
    }
}
