using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui;

namespace bombs_away.component.physics
{
    class RigidBody : IRigidBody
    {
        private float acceleration = -9.81f;
        private float jumpAcc = 1;
        private float velocity = 0;
        private const float JUMP_ACC = -20;

        private bool grounded = false;
        private GameObject gameObject;

        public void Initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void AddForceX(float force)
        {
            throw new NotImplementedException();
        }

        public void AddForceY(float force)
        {
            throw new NotImplementedException();
        }

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

        public void Execute(float updatePeriod)
        {
            //Console.WriteLine(velocity);
            velocity = acceleration * jumpAcc * updatePeriod;

            MoveY(velocity * updatePeriod);
        }

        private void MoveX(float value)
        {
            gameObject.Body.MinX += value;
        }

        private void MoveY(float value)
        {
            gameObject.Body.MinY += value;
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
