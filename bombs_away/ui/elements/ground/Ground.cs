using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.ground
{
    public class Ground : GameObject
    {
        public Ground(Vector2 position, float squareSize)
        {
            this.body = Box2DFactory.CreateSquare(position, squareSize);
        }
    }
}
