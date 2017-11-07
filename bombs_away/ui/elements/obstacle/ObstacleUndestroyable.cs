using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.obstacle
{
    class ObstacleUndestroyable : Obstacle
    {
        public ObstacleUndestroyable(Vector2 position)
        {
            this.component = Box2DFactory.CreateSquare(position, 0.2f);
        }
    }
}
