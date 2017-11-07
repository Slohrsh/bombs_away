using bombs_away.controller;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui.elements.obstacle;
using OpenTK;

namespace bombs_away
{
    class GameMapBuilder
    {
        public GameLogic GetState()
        {
            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0,0)));
            return new GameLogic(new Player(new Vector2(0f, -0.5f)), null, obstacles, null);
        }
    }
}