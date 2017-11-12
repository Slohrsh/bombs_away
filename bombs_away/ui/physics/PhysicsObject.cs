using bombs_away.ui.openGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.physics
{
    class PhysicsObject : Drawable
    {
        private float acceleration = 98.1f;
        private float jumpAcc = 10;
        private float velocity = 0;

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
            velocity = acceleration * updatePeriod;

            ShiftDown(velocity * updatePeriod);
        }

        protected void Jump(float updatePeriod)
        {
            if (!grounded)
            {
                return;
            }
            ShiftUp(jumpAcc * updatePeriod);
            Grounded = false;
        }

        private void ShiftUp(float value)
        {
            if (component.MaxY < 1)
            {
                component.MinY += value;
            }
        }

        private void ShiftDown(float value)
        {
            if (component.MinY > -1 && !grounded)
            {
                component.MinY -= value;
            }
        }

    }
}
