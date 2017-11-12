using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class Patrol
    {
        public event EventHandler OnChangeDirectionLeft;
        public event EventHandler OnChangeDirectionRight;
        private bool isMovingRight;

        public float updatePeriod { get;  set; }
        public float patrolTime { get; set; }

        private float timeDelta;
        private float absoluteTime;

        public Patrol(float patrolTime)
        {
            this.patrolTime = patrolTime;
        }


        public void Execute(float updatePeriod)
        {
            absoluteTime += updatePeriod;
            if(absoluteTime % patrolTime < 0.05 && absoluteTime % patrolTime > 0)
            {
                Console.WriteLine(isMovingRight);
                isMovingRight = !isMovingRight;
            }
            if (isMovingRight)
            {
                OnChangeDirectionLeft?.Invoke(this, null);
            }
            else
            {
                OnChangeDirectionRight?.Invoke(this, null);
            }
            this.updatePeriod = updatePeriod;
        }
    }
}
