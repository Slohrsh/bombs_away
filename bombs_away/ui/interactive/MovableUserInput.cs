using OpenTK.Input;
using System;

namespace bombs_away.ui.interactive
{
    class MovableUserInput : Moveable
    {

        public event EventHandler onPlantBomb;

        private float timeDelta;

        private void HandleUserInput(float updatePeriod)
        {

            timeDelta += updatePeriod;

            if (Keyboard.GetState()[Key.Left] || Keyboard.GetState()[Key.A])
            {
                ShiftLeft(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Right] || Keyboard.GetState()[Key.D])
            {
                ShiftRight(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Space])
            {
                Jump(updatePeriod);
            }
            if(Keyboard.GetState()[Key.E])
            {
                if(timeDelta > 3f)
                {
                    plantBomb();
                    timeDelta = 0;
                }
            }
        }


        public void plantBomb()
        {
            onPlantBomb?.Invoke(this, null);
        }

        public override void Execute(float updatePeriod)
        {
            HandleUserInput(updatePeriod);
            base.Execute(updatePeriod);
        }
    }
}
