using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using System;
using System.Linq;
using OpenTK;
using bombs_away.ui.interactive;

namespace bombs_away.game
{
    class GameLogic
    {
        public event EventHandler onLost;
        public event EventHandler onThrowBomb;
        public event EventHandler onEnemyDestroy;
        private bool isGameOver = false;

        private Level level;

        public GameLogic(Level level)
        {
            this.level = level;
            level.player.onPlantBomb += (sender, args) => plantBomb(sender, args);
        }

        private void plantBomb(object sender, EventArgs args)
        {
            Player player = (Player)sender;
            level.bombs.Add(new BombBigRadius(new Vector2(player.Component.MinX, player.Component.MinY),
                player.Component.SizeX));
        }

        public void Update(float updatePeriod)
        {
            if (!isGameOver)
            {
                ExecuteAllElements(updatePeriod);
                HandleCollisions(updatePeriod);
                if (!level.enemies.Any())
                {
                    ShowPortal();
                }
            }
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
        private float timeDelta;
        private void HandleCollisions(float updatePeriod)
        {
            level.player.Grounded = false;

            if(level.portal.IsVisible)
            {
                if(level.player.Intersects(level.portal))
                {
                    Win();
                }
            }
           
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
                        Lost();
                    }

                    if (enemy.Intersects(obstacle))
                    {
                        Directions pushDirection = Box2DextensionsCustom.UndoOverlap(enemy.Component, obstacle.Component);
                        Console.WriteLine(pushDirection);
                        enemy.Grounded = true;
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
                        timeDelta += updatePeriod;
                        if (bomb.Intersects(level.player))
                        {
                            Console.WriteLine("Oh snap! I committed suicide!");
                            Lost();
                        }
                        if(timeDelta > 3)
                        {
                            level.bombs.Remove(bomb);
                            timeDelta = 0;
                        }
                    }
                }
            }
        }

        private void ShowPortal()
        {
            level.portal.setVisible();
        }

        private void Lost()
        {
            isGameOver = true;
            onLost?.Invoke(this, null);
        }
        private void Win()
        {
            isGameOver = true;
        }
    }
}