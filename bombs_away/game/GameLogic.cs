using bombs_away.ui.elements;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.enums;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.controller
{
    class GameLogic
    {
        public event EventHandler onLost;
        public event EventHandler onThrowBomb;
        public event EventHandler onEnemyDestroy;

        private Player player;
        private List<Enemy> enemies;
        private List<Obstacle> obstacles;
        private List<Bomb> bombs;

        public Player Player { get { return player; }}
        public List<Enemy> Enemies { get { return enemies; } }
        public List<Obstacle> Obstacles { get { return obstacles; } }
        public List<Bomb> Bombs { get { return bombs; } }

        public GameLogic(Player player, List<Enemy> enemies, List<Obstacle> obstacles, List<Bomb> bombs)
        {
            this.player = player;
            this.enemies = enemies != null ? enemies : new List<Enemy>();
            this.obstacles = obstacles != null ? obstacles : new List<Obstacle>();
            this.bombs = bombs != null ? bombs : new List<Bomb>(); ;
        }

        public void Update(float updatePeriod)
        {
            ExecuteAllElements(updatePeriod);
            HandleCollisions();
            /*foreach(Obstacle obstacle in obstacles)
            {
                obstacle.Execute(Movement.IDLE, absoluteTime, updatePeriod);
            }*/
        }

        private void ExecuteAllElements(float updatePeriod)
        {
            player.Execute(updatePeriod);
            foreach (Enemy enemy in enemies)
            {
                enemy.Execute(updatePeriod);
            }
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Execute(updatePeriod);
            }
        }

        private void HandleCollisions()
        {
            foreach(Enemy enemy in enemies)
            {
                if(enemy.Intersects(player))
                {
                    Console.WriteLine("Lost");
                    onLost?.Invoke(this, null);
                }
                foreach (Bomb bomb in bombs)
                {
                    if (bomb.State == BombState.EXPLODE)
                    {
                        if (bomb.Intersects(enemy))
                        {
                            onEnemyDestroy?.Invoke(this, null);
                        }
                        if(bomb.Intersects(player))
                        {
                            onLost?.Invoke(this, null);
                        }
                    }
                }
                foreach (Obstacle obstacle in obstacles)
                {
                    if(enemy.Intersects(obstacle))
                    {
                        Box2dExtensions.UndoOverlap(enemy.Component, obstacle.Component);
                    }
                    if (obstacle.Intersects(player))
                    {
                        Console.WriteLine(obstacle.Intersects(player));
                        player.Grounded = true;
                        Box2dExtensions.UndoOverlap(player.Component, obstacle.Component);
                        //Aus overlap schauen ob er auf dem Obstacle steht -> Overlap in Y richtung
                        //Box2dExtensions.Overlap(player.Component, obstacle.Component)
                    }
                    else
                    {
                        player.Grounded = false;
                    }
                }
            }
        }
    }
}
