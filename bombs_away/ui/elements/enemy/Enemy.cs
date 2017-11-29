using bombs_away.ui.interactive;
using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;
using bombs_away.game;
using bombs_away.ui.enums;

namespace bombs_away.ui.elements.enemy
{
    class Enemy : MovableStaticMoves
    {
        public Enemy(Vector2 position, float squareSize)
        {
            this.component = Box2DFactory.CreateSquare(position, squareSize);
        }
        public Enemy(Box2D component)
        {
            this.component = new Box2D(component);
        }

        protected override void UndoOverlap(Block block)
        {
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Component;
                if (component.Intersects(ground))
                {
                    Directions pushDirection =
                        Box2DextensionsCustom.UndoOverlap(component, ground);
                    if (pushDirection == Directions.UP)
                    {
                        Grounded = true;
                    }
                    if(pushDirection == Directions.LEFT 
                        || pushDirection == Directions.RIGHT)
                    {
                        IsMovingRight = !IsMovingRight;
                    }
                }
            }
        }
    }
}
