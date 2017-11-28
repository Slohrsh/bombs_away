using bombs_away.game;
using bombs_away.ui.enums;
using bombs_away.ui.physics;
using bombs_away.ui.zenseless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.interactive
{
    class Moveable : GameObject
    {

        protected virtual void MoveX(float value)
        {
            body.MinX += value;
        }

        protected virtual void MoveY(float value)
        {
            body.MinY += value;
        }
    }
}
