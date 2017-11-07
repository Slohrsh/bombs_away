using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.zenseless
{
    static class Box2DFactory
    {
        public static Box2D CreateSquare(Vector2 position, float size)
        {
            return new Box2D(position.X, position.Y, size, size);
        }
    }
}
