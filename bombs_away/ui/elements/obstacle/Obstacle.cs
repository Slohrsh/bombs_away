using bombs_away.ui.physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;
using OpenTK.Graphics;
using bombs_away.ui.zenseless;
using OpenTK;

namespace bombs_away.ui.elements.obstacle
{
    class Obstacle : PhysicsObject
    {
        public Obstacle(Vector2 position, float squareSize)
        {
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize);
        }
        public Obstacle(Box2D component)
        {
            this.Bounds = new Box2D(component);
        }
    }
}
