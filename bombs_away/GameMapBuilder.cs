using bombs_away.controller;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away
{
    class GameMapBuilder
    {
        public GameLogic GetState()
        {
            return new GameLogic(new Player(), null, null, null);
        }
    }
}
