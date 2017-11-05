using bombs_away.ui.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui
{
    class Movable : Drawable
    {
        private enum State { JUMPING, WALKING, IN_AIR }
        private State state = State.WALKING;
        float timeDelta = 0;
        float timeOnJumpSwitched = 0;

        public void setStateWalking()
        {
            state = State.WALKING;
            timeDelta = 0;
        }
        public virtual void Execute(Movement movement, float absoluteTime, float value)
        {
            switch (movement)
            {
                case Movement.UP:
                    ShiftUp(value);
                    break;
                case Movement.DOWN:
                    ShiftDown(value);
                    break;
                case Movement.LEFT:
                    ShiftLeft(value);
                    break;
                case Movement.RIGTH:
                    ShiftRight(value);
                    break;
                case Movement.JUMP:
                    Jump(absoluteTime);
                    break;
            }
            if (state != State.JUMPING)
            {
                ShiftDown(value);
            }
            if (state == State.JUMPING)
            {
                timeDelta = absoluteTime - timeOnJumpSwitched;
                Console.WriteLine(timeDelta);
                if (timeDelta < 0.3f)
                {
                    ShiftUp(value);
                }
                else
                {
                    state = State.IN_AIR;
                }
            }
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
            if (component.MinY > -1)
            {
                component.MinY -= value;
            }
        }

        public void Jump(float absoluteTime)
        {
            if (state == State.WALKING)
            {
                state = State.JUMPING;
                timeOnJumpSwitched = absoluteTime;
            }
        }
    }
}
