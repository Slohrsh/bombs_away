﻿using bombs_away.ui;
using bombs_away.ui.interactive;
using System;

namespace bombs_away.ui.physics
{
    class PhysicsObject : Moveable
    {
        
        private float acceleration = -9.81f;
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

        public virtual void Execute(float updatePeriod)
        {
            Console.WriteLine(velocity);
            velocity += acceleration * jumpAcc * updatePeriod;

            MoveY(velocity * updatePeriod);
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
    }
}
