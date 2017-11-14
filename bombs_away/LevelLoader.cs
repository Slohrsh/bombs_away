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
using bombs_away.ui.elements.bomb;
using bombs_away.game;

namespace bombs_away
{
    class LevelLoader
    {
        public Level Load()
        {
            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-1f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.8f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.6f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.4f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.2f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.2f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.4f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.6f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.8f, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(1, -1f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-1, -0.8f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.8f, -0.8f)));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0, -0.4f)));
           
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy;
            enemy = new Enemy(new OpenTK.Vector2(0, -0.8f));
            enemies.Add(enemy);

            List<Bomb> bombs = new List<Bomb>();
            bombs.Add(new BombBigRadius(new OpenTK.Vector2(0.3f, 0)));

            return new Level(new Player(new Vector2(0f, 0f)), enemies, obstacles, bombs);
        }
    }
}