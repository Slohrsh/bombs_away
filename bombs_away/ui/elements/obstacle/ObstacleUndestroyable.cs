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
        public ObstacleUndestroyable(Vector2 position, float squareSize)
        {
            this.component = Box2DFactory.CreateSquare(position, squareSize);
        }

        public override void Execute(float updatePeriod)
        {
            
        }
    }
}
