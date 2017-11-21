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
using bombs_away.ui.elements.portal;

namespace bombs_away
{
    class LevelLoader
    {
        private float squareSize;
        public Level Load()
        {
            float levelSize = CalculateAmountOfBlocksInXDirection();
            squareSize = CalculateSquareSize(levelSize);

            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-1f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.8f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.6f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.4f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-0.2f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.2f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.4f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.6f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.8f, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(1, -1f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(-1, -0.8f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0.8f, -0.8f), squareSize));
            obstacles.Add(new ObstacleUndestroyable(new Vector2(0, -0.4f), squareSize));
           
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy;
            enemy = new Enemy(new Vector2(0, -0.8f), squareSize);
            enemies.Add(enemy);

            List<Bomb> bombs = new List<Bomb>();
            //bombs.Add(new BombBigRadius(new Vector2(0.3f, 0), squareSize)); 

            return new Level(new Player(new Vector2(0f, 0f), squareSize), enemies, obstacles, bombs, new Portal(new Vector2(-0.4f, -0.6f), squareSize));
        }

        private float CalculateSquareSize(float levelSize)
        {
            return 2/levelSize;
        }

        private float CalculateAmountOfBlocksInXDirection()
        {
            return 10;
        }
    }
}