using bombs_away.game;
using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.interactive
{
    class Colidable : Moveable
    {
        public virtual void ResolveCollision()
        {
            ModelView model = ModelView.Instance;
            
            float huhu = TransformPositionRelative(component.CenterY, 0);
            
            UndoOverlapRelativeToComponent(-1, -1, model);
            UndoOverlapRelativeToComponent(0, -1, model);
            UndoOverlapRelativeToComponent(1, -1, model);

            UndoOverlapRelativeToComponent(-1, 1, model);
            UndoOverlapRelativeToComponent(0, 1, model);
            UndoOverlapRelativeToComponent(1, 1, model);

            UndoOverlapRelativeToComponent(-1, 0, model);
            UndoOverlapRelativeToComponent(0, 0, model);
            UndoOverlapRelativeToComponent(1, 0, model);

        }

        private void UndoOverlapRelativeToComponent(int x, int y, ModelView model)
        {
            int positionX = TransformPositionRelative(component.CenterX, x);
            int positionY = TransformPositionRelative(component.CenterY, y);

            if (positionX >= 0 && positionX <= (int)StaticValues.GRIDSIZE-1 &&
                positionY >= 0 && positionY <= (int)StaticValues.GRIDSIZE-1)
                {
                    if (positionX == 12 && positionY == 18)
                    {
                        Console.WriteLine("huhu");
                    }
                Block block = model.StaticGrid[positionX, positionY];
                UndoOverlap(block);
            }
        }

        private int TransformPositionRelative(float componentPosition, int position)
        {
            int relativePosition = (int)(componentPosition * (int)StaticValues.GRIDSIZE);
            return relativePosition + position;
        }

        protected virtual void UndoOverlap(Block block)
        {
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Component;
                if (component.Intersects(ground))
                {
                    Directions pushDirection = 
                        Box2DextensionsCustom.UndoOverlap(component, ground);
                }
            }
        }
    }
}
