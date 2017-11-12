using bombs_away.ui.enums;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class MovableUserInput : Moveable
    {
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

        public override void Execute(float updatePeriod)
        {
            HandleUserInput(updatePeriod);
            base.Execute(updatePeriod);
        }
    }
}
