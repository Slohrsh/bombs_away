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
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize - (squareSize / 1.9f));
        }
        public Enemy(Box2D component)
        {
            this.Bounds = new Box2D(component);
        }

        protected override void HandleGroundCollision(Block block)
        {
            Box2D ground = block.Bounds;
            Directions pushDirection =
                Box2DextensionsCustom.UndoOverlap(Bounds, ground);
            if (pushDirection == Directions.UP)
            {
                Grounded = true;
            }
        }
        
        protected override void HandleInvisibleEnemyBarrierCollision(Block block)
        {
            Box2D ground = block.Bounds;
            Directions pushDirection =
                Box2DextensionsCustom.UndoOverlap(Bounds, ground);
            
            if(pushDirection == Directions.LEFT 
               || pushDirection == Directions.RIGHT)
            {
                IsMovingRight = !IsMovingRight;
            }
        }
    }
}
