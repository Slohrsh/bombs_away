using bombs_away.game;
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
        ModelView modelView = ModelView.Instance;
        public BombBigRadius(Vector2 position, float squareSize)
        {
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize);
            this.timeDeltaToExplode = 1;
        }

        public override void Explode(float updatePeriod)
        {
            Bounds.MaxX += (updatePeriod * 2) / modelView.gridSize;
            Bounds.MinX -= updatePeriod / modelView.gridSize;
            base.Explode(updatePeriod);
        }
    }
}
