using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.bomb
{
    class BombBigRadius : Bomb
    {
        public BombBigRadius(Vector2 position, float squareSize)
        {
            this.component = Box2DFactory.CreateSquare(position, squareSize);
            this.timeDeltaToExplode = 3;
        }

        public override void Explode(float updatePeriod)
        {
            component.MaxX += updatePeriod * 2;
            component.MinX -= updatePeriod;
            base.Explode(updatePeriod);
        }
    }
}
