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
        ModelView model = ModelView.Instance;

        public virtual void ResolveCollision()
        {
            
            float huhu = TransformPositionRelative(Bounds.CenterY, 0);
            
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
            int positionX = TransformPositionRelative(Bounds.CenterX, x);
            int positionY = TransformPositionRelative(Bounds.CenterY, y);

            if (positionX >= 0 && positionX <= model.gridSize-1 &&
                positionY >= 0 && positionY <= model.gridSize-1)
                {
                foreach(Block block in model.ConstantGrid[positionX, positionY])
                {
                    UndoOverlap(block);
                }
            }
        }

        private int TransformPositionRelative(float componentPosition, int position)
        {
            int relativePosition = (int)(componentPosition * model.gridSize);
            return relativePosition + position;
        }

        protected virtual void UndoOverlap(Block block)
        {
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Bounds;
                if (Bounds.Intersects(ground))
                {
                    Directions pushDirection = 
                        Box2DextensionsCustom.UndoOverlap(Bounds, ground);
                }
            }
        }
    }
}
