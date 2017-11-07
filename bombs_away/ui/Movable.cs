using bombs_away.ui.enums;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui
{
    class Movable : Drawable
    {

        float acceleration = 98.1f;
        float jumpAcc = 10;
        float velocity = 0;

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
            } }

        private void HandleUserInput(float updatePeriod)
        {
            if (Keyboard.GetState()[Key.Left])
            {
                ShiftLeft(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Right])
            {
                ShiftRight(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Space])
            {
                Jump(updatePeriod);
            }
        }

        public virtual void Execute(float updatePeriod)
        {
            velocity = acceleration * updatePeriod;
            HandleUserInput(updatePeriod);
            
            ShiftDown(velocity * updatePeriod);
            Console.WriteLine(velocity* updatePeriod);
        }

        public void ShiftLeft(float value)
        {
            if (component.MinX > -1)
            {
                component.MinX -= value;
            }
        }

        public void ShiftRight(float value)
        {
            if (component.MaxX < 1)
            {
                component.MinX += value;
            }
        }

        public void ShiftUp(float value)
        {
            if (component.MaxY < 1)
            {
                component.MinY += value;
            }
        }

        public void ShiftDown(float value)
        {
            if (component.MinY > -1&& !grounded)
            {
                component.MinY -= value;
            }
        }

        public void Jump(float updatePeriod)
        {
            if (!grounded)
            {
                return;
            }
            ShiftUp(jumpAcc * updatePeriod);
            Grounded = false;
            /*if (state == State.WALKING)
            {
                state = State.JUMPING;
                timeOnJumpSwitched = absoluteTime;
            }*/
        }
    }
}
