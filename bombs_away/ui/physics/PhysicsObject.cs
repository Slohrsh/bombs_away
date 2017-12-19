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
        
        private float acceleration = -981f;
        private float jumpAcc = 1;
        protected float velocity = 0;
        private const float JUMP_ACC = -28;

        private bool grounded = false;
        
        ModelView modelView = ModelView.Instance;

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

            velocity += (acceleration * updatePeriod * updatePeriod) / modelView.gridSize;

            MoveY(velocity);
            return Position;
        }

        protected void Jump(float updatePeriod)
        {
            if (grounded)
            {
                velocity += (acceleration * JUMP_ACC * updatePeriod * updatePeriod) / modelView.gridSize;
                //MoveY(velocity);
                Grounded = false;
            }
        }
        protected override void UndoOverlap(Block block)
        {
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Bounds;
                if (Bounds.Intersects(ground))
                {
                    Directions pushDirection =
                        Box2DextensionsCustom.UndoOverlap(Bounds, ground);
                    if(pushDirection == Directions.UP)
                    {
                        Grounded = true;
                    }
                    if(pushDirection == Directions.DOWN)
                    {
                        velocity = 0;
                    }
                }
            }
        }
    }
}
