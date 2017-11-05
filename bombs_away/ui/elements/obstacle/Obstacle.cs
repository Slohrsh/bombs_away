using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.obstacle
{
    class Obstacle : Drawable
    {
        public Obstacle()
        {
            this.component = new Box2D(0.0f, -0.95f, 0.2f, 0.2f);
        }
    }
}
