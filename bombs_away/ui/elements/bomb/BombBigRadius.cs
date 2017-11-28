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
            this.body = Box2DFactory.CreateSquare(position, squareSize);
            this.timeDeltaToExplode = 1;
        }

        public override void Explode(float updatePeriod)
        {
            body.MaxX += updatePeriod * 2;
            body.MinX -= updatePeriod;
            base.Explode(updatePeriod);
        }
    }
}
