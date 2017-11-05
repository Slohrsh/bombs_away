using bombs_away.controller;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui.elements.obstacle;

namespace bombs_away
{
    class GameMapBuilder
    {
        public GameLogic GetState()
        {
            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.Add(new Obstacle());
            return new GameLogic(new Player(), null, obstacles, null);
        }
    }
}
