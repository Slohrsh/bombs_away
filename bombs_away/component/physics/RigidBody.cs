using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui;
using OpenTK;

namespace bombs_away.component.physics
{
    class RigidBody : IRigidBody
    {
        private const float EARTH_MASS = 10;
        private const float DEFAULT_MASS = 1;

        private Vector2 force;
        private float mass;

        private GameObject gameObject;

        public void Initialize(GameObject gameObject, float mass)
        {
            this.gameObject = gameObject;
            this.mass = mass;
        }

        public void AddForce(Vector2 direction)
        {
            force.X += direction.X;
            force.Y += direction.Y;
        }

        public void Execute(float updatePeriod)
        {
            Console.WriteLine(force.Y);
            if(gameObject.Body.CenterY > 0)
            {
                float distance = gameObject.Body.CenterY * 1000;
                force.Y -= (mass * EARTH_MASS) / (distance * distance);
                Move(force);
            }

        }

        private void Move(Vector2 direction)
        {
            gameObject.Body.MinX += direction.X;
            gameObject.Body.MinY += direction.Y;
        }

        public void Initialize(GameObject gameObject)
        {
            Initialize(gameObject, DEFAULT_MASS);
        }
    }
}
