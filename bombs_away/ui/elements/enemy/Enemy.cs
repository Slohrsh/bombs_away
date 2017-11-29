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
using bombs_away.component.interactive;

namespace bombs_away.ui.elements.enemy
{
    class Enemy : MovableStaticMoves
    {
        public Enemy(Vector2 position, float squareSize)
        {
            this.body = Box2DFactory.CreateSquare(position, squareSize);
            RegisterComponents();
        }
        public Enemy(Box2D component)
        {
            this.body = new Box2D(component);
            RegisterComponents();
        }
        private void RegisterComponents()
        {
            ICollider collider = new Collider();
            collider.Initialize(this);
            collider.onCollision += (sender, args) => UndoOverlap(sender, args);
            components.Add(collider);
        }

        private void UndoOverlap(object sender, EventArgs args)
        {
            Block block = (Block)sender;
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Component;
                if (body.Intersects(ground))
                {
                    Directions pushDirection =
                        Box2DextensionsCustom.UndoOverlap(body, ground);
                    if (pushDirection == Directions.UP)
                    {
                        Grounded = true;
                    }
                    if(pushDirection == Directions.LEFT || pushDirection == Directions.RIGHT)
                    {
                        IsMovingRight = !IsMovingRight;
                    }
                }
            }
        }
    }
}
