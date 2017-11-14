using bombs_away.ui.physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;
using OpenTK.Graphics;

namespace bombs_away.ui.elements.obstacle
{
    abstract class Obstacle : PhysicsObject, IDrawable
    {
        public Box2D Bounds => throw new NotImplementedException();

        public Color4 Color => throw new NotImplementedException();
    }
}
