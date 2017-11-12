using bombs_away.controller;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui.elements.obstacle;
using OpenTK;
using bombs_away.ui.elements.enemy;

namespace bombs_away
{
    class GameMapBuilder
    {
        public GameLogic GetState()
        {
            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0,-1)));

            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy;
            enemy = new Enemy(new OpenTK.Vector2(0, 1));
            enemy.Patrol = new ui.interactive.Patrol(2f);
            enemies.Add(enemy);
            return new GameLogic(new Player(new Vector2(0f, 0f)), enemies, obstacles, null);
        }
    }
}