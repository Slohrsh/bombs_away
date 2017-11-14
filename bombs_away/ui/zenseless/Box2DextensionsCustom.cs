using bombs_away.ui.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.zenseless
{
    static class Box2DextensionsCustom
    {
        public static Directions UndoOverlap(this Box2D rectangleA, Box2D rectangleB)
        {
            if (!rectangleA.Intersects(rectangleB)) return Directions.NO_DIRECTION;

            Vector2[] directions = new Vector2[]
            {
                new Vector2(rectangleB.MaxX - rectangleA.MinX, 0), // Rechts
				new Vector2(rectangleB.MinX - rectangleA.MaxX, 0), // Links
				new Vector2(0, rectangleB.MaxY - rectangleA.MinY), // Oben 
				new Vector2(0, rectangleB.MinY - rectangleA.MaxY)  // Unten 
			};
            float[] pushDistSqrd = new float[4];
            for (int i = 0; i < 4; ++i)
            {
                pushDistSqrd[i] = directions[i].LengthSquared();
            }
            //find minimal positive overlap amount
            int minId = 0;
            for (int i = 1; i < 4; ++i)
            {
                minId = pushDistSqrd[i] < pushDistSqrd[minId] ? i : minId;
            }

            rectangleA.MinX += directions[minId].X;
            rectangleA.MinY += directions[minId].Y;

            switch (minId)
            {
                case 0:
                    return Directions.RIGHT;
                case 1:
                    return Directions.LEFT;
                case 2:
                    return Directions.UP;
                case 3:
                    return Directions.DOWN;
                default:
                    return Directions.NO_DIRECTION;
            }
        }
    }
}
