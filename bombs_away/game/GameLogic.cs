using bombs_away.game;
using bombs_away.ui.elements;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.game
{
    class GameLogic
    {
        public event EventHandler onLost;
        public event EventHandler onThrowBomb;
        public event EventHandler onEnemyDestroy;

        private Level level;

        public GameLogic(Level level)
        {
            this.level = level;
        }

        public void Update(float updatePeriod)
        {
            ExecuteAllElements(updatePeriod);
            HandleCollisions();
        }

        private void ExecuteAllElements(float updatePeriod)
        {
            level.player.Execute(updatePeriod);
            foreach (Enemy enemy in level.enemies)
            {
                enemy.Execute(updatePeriod);
            }
            foreach (Obstacle obstacle in level.obstacles)
            {
                obstacle.Execute(updatePeriod);
            }
            foreach (Bomb bomb in level.bombs)
            {
                bomb.Execute(updatePeriod);
            }
        }

        private void HandleCollisions()
        {
            level.player.Grounded = false;

            foreach (Obstacle obstacle in level.obstacles)
            {
                if (level.player.Intersects(obstacle))
                {
                    Directions pushDirection = Box2DextensionsCustom.UndoOverlap(level.player.Component, obstacle.Component);
                    if (pushDirection == Directions.UP)
                    {
                        level.player.Grounded = true;
                    }
                }

                

                foreach (Enemy enemy in level.enemies.ToList())
                {
                    if (enemy.Intersects(level.player))
                    {
                        Console.WriteLine("Lost");
                        onLost?.Invoke(this, null);
                    }

                    if (enemy.Intersects(obstacle))
                    {
                        Directions pushDirection = Box2DextensionsCustom.UndoOverlap(enemy.Component, obstacle.Component);
                        if (pushDirection == Directions.LEFT || pushDirection == Directions.RIGHT)
                        {
                            enemy.IsMovingRight = !enemy.IsMovingRight;
                        }

                    }

                    foreach (Bomb bomb in level.bombs.ToList())
                    {
                        if (bomb.State == BombState.EXPLODE)
                        {
                            if (bomb.Intersects(enemy))
                            {
                                level.enemies.Remove(enemy);
                                Console.WriteLine("Wuhuu! I've killed the Enemy!");
                                onEnemyDestroy?.Invoke(this, null);
                            }
                        }
                    }
                }
                foreach (Bomb bomb in level.bombs.ToList())
                {
                    if (bomb.Intersects(obstacle))
                    {
                        bomb.Grounded = true;
                        Directions pushDirection = Box2DextensionsCustom.UndoOverlap(bomb.Component, obstacle.Component);
                    }

                    if (bomb.State == BombState.EXPLODE)
                    {
                        if (bomb.Intersects(level.player))
                        {
                            Console.WriteLine("Oh snap! I committed suicide!");
                            onLost?.Invoke(this, null);
                        }
                        level.bombs.Remove(bomb);
                    }
                }
            }
        }
    }
}