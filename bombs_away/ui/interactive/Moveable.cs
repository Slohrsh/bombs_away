using bombs_away.ui.physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class Moveable : PhysicsObject
    {
        protected void ShiftLeft(float value)
        {
            if (component.MinX > -1)
            {
                component.MinX -= value;
            }
        }

        protected void ShiftRight(float value)
        {
            if (component.MaxX < 1)
            {
                component.MinX += value;
            }
        }
    }
}
