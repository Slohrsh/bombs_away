using bombs_away.game;
using bombs_away.ui;
using bombs_away.ui.enums;
using bombs_away.ui.interactive;
using bombs_away.ui.zenseless;
using OpenTK;
using System;
using Zenseless.Geometry;

namespace bombs_away.ui.physics
{
    class PhysicsObject : Colidable
    {
        
        private float acceleration = -0.981f;
        private float jumpAcc = 1;
        private float velocity = 0;
        private const float JUMP_ACC = -20;

        private bool grounded = false;

        public bool Grounded
        {
            set
            {
                if (value)
                {
                    velocity = 0f;
                    grounded = value;
                }
                grounded = value;
            }
        }

        public virtual Vector2 Execute(float updatePeriod)
        {
            //Console.WriteLine(velocity);
            velocity += acceleration * jumpAcc * updatePeriod;

            MoveY(velocity * updatePeriod);
            return Position;
        }

        protected void Jump(float updatePeriod)
        {
            if (!grounded)
            {
                jumpAcc = 1;
                return;
            }
            jumpAcc = JUMP_ACC;
            Grounded = false;
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
                    if(pushDirection == Directions.UP)
                    {
                        Grounded = true;
                    }
                }
            }
        }
    }
}
