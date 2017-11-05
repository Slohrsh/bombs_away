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

        public void Update(float absoluteTime, float updatePeriod, Movement movement)
        {
            HandleCollisions();
            player.Execute(movement, absoluteTime, updatePeriod);
            /*foreach(Obstacle obstacle in obstacles)
            {
                obstacle.Execute(Movement.IDLE, absoluteTime, updatePeriod);
            }*/
        }

        private void HandleCollisions()
        {
            foreach(Enemy enemy in enemies)
            {
                if(player.Intersects(enemy))
                {
                    onLost?.Invoke(this, null);
                }
               
                foreach (Bomb bomb in bombs)
                {
                    if (bomb.State == BombState.EXPLODE)
                    {
                        if (enemy.Intersects(bomb))
                        {
                            onEnemyDestroy?.Invoke(this, null);
                        }
                        if(player.Intersects(bomb))
                        {
                            onLost?.Invoke(this, null);
                        }
                    }
                }
            }
            foreach(Obstacle obstacle in obstacles)
            {
                
                if (obstacle.Intersects(player))
                {
                    Console.WriteLine(obstacle.Intersects(player));
                    player.setStateWalking();
                }
            }
        }
    }
}
