using bombs_away.ui.enums;
using bombs_away.ui.physics;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class MovableStaticMoves : PhysicsObject
    {
        private bool isMovingRight = true;
        private float speed = 0.3f;
        public bool IsMovingRight { get { return isMovingRight; } set { isMovingRight = value; } }

        public override void Execute(float updatePeriod)
        {
            if (isMovingRight)
            {
                MoveX(updatePeriod * speed);
            }
            else
            {
                MoveX(-1*updatePeriod*speed);
            }
            base.Execute(updatePeriod);
        }
    }
}
