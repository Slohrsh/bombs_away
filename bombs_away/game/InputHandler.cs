using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace bombs_away.game
{
    class InputHandler
    {
        private readonly GameWindow gameWindow;
        public InputHandler(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;

            gameWindow.KeyDown += (sender, args) => KeyDown(args.Key);
        }

        private void KeyDown(Key key)
        {
            
        }
    }
}
