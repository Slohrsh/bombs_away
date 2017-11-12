using bombs_away.ui.enums;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.interactive
{
    class MovableStaticMoves : Moveable
    {
        private Patrol patrol;
        public Patrol Patrol {
            set
            {
                patrol = value;
                patrol.OnChangeDirectionLeft += (sender, args) => { ShiftLeft(sender, args); };
                patrol.OnChangeDirectionRight += (sender, args) => { ShiftRight(sender, args); };
            }
        }

        private void ShiftLeft(object sender, EventArgs args)
        {
            Patrol patrol = (Patrol)sender;
            ShiftLeft(patrol.updatePeriod);
        }

        private void ShiftRight(object sender, EventArgs args)
        {
            Patrol patrol = (Patrol)sender;
            ShiftRight(patrol.updatePeriod);
        }

        public override void Execute(float updatePeriod)
        {
            if(patrol != null)
            {
                patrol.Execute(updatePeriod);
            }
            base.Execute(updatePeriod);
        }
    }
}
