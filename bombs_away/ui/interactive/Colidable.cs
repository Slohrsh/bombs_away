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
        private ModelView model = ModelView.Instance;
        public event EventHandler onEnemyCollision;
        public event EventHandler onBombCollision;
        public event EventHandler onPortalCollision;


        public virtual void ResolveCollision()
        {
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
            int positionX = GridUtil.TransformPositionRelative(Bounds.CenterX, x, model.gridSize);
            int positionY = GridUtil.TransformPositionRelative(Bounds.CenterY, y, model.gridSize);

            if (positionX >= 0 && positionX <= model.gridSize-1 &&
                positionY >= 0 && positionY <= model.gridSize-1)
                {
                foreach(Block block in model.Grid[positionX, positionY].ToArray())
                {
                    if(block.Bounds.Equals(Bounds))
                    {
                        continue;
                    }
                    if (!IsIntersection(block.Bounds, Bounds))
                    {
                        continue;
                    }
                    switch (block.Type)
                    {
                        case BlockType.GROUND:
                            HandleGroundCollision(block);
                            break;
                        case BlockType.OBSTACLE:
                            HandleGroundCollision(block);
                            break;
                        case BlockType.ENEMY:
                            onEnemyCollision?.Invoke(block, null);
                            break;
                        case BlockType.PLAYER:
                            break;
                        case BlockType.BOMB:
                            onBombCollision?.Invoke(this, null);
                            break;
                        case BlockType.PORTAL:
                            onPortalCollision?.Invoke(block, null);
                            break;
                    }
                }
            }
        }

        private bool IsIntersection(Box2D a, Box2D b)
        {
            return a.Intersects(b);
        }

        protected virtual void HandleGroundCollision(Block block)
        {
            Box2D ground = block.Bounds;
            Box2DextensionsCustom.UndoOverlap(Bounds, ground);
        }
    }
}
