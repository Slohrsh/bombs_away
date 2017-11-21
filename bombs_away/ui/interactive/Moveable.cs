using bombs_away.ui.physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class Moveable : GameObject
    {
        protected void MoveX(float value)
        {
            component.MinX += value;
        }

        protected void MoveY(float value)
        {
            component.MinY += value;
        }
    }
}
