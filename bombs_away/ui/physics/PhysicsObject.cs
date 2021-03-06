﻿using bombs_away.game;
using bombs_away.ui.enums;
using bombs_away.ui.interactive;
using bombs_away.ui.zenseless;
using OpenTK;
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

        public override Vector2 Execute(float updatePeriod)
        {
            base.Execute(updatePeriod);
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
        protected override void HandleGroundCollision(Block block)
        {
            Box2D ground = block.Bounds;
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
 