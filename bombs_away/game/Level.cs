using bombs_away.ui;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.elements.portal;
using bombs_away.ui.enums;
using bombs_away.util;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenseless.Geometry;

namespace bombs_away.game
{
    class Level
    {
        public event EventHandler onLost;
        public event EventHandler onThrowBomb;
        public event EventHandler onEnemyDestroy;
        public bool IsGameOver { get { return isGameOver; } }
        private bool isGameOver = false;
        private ModelView modelView = ModelView.Instance;
        private Player player;
        private List<Enemy> enemies;
        private List<Obstacle> obstacles;
        private List<Bomb> bombs;
        private Portal portal;
        private int availableAmountOfBombs = 1;

        public Level(List<Block>[,] grid)
        {
            modelView.Grid = grid;
            GenerateImplementation(grid);
        }

        internal void Execute(float updatePeriod)
        {
            ExecuteAllElements(updatePeriod);
            HandleCollisions(updatePeriod);
        }

        public void ExecuteAllElements(float updatePeriod)
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
            foreach (Bomb bomb in bombs)
            {
                bomb.Execute(updatePeriod);
            }
            if (!enemies.Any())
            {
                portal.setVisible();
                int x = GridUtil.TransformPositionRelative(portal.Bounds.CenterX, 0, modelView.gridSize);
                int y = GridUtil.TransformPositionRelative(portal.Bounds.CenterY, 0, modelView.gridSize);
                portal.Bounds.SetReferencedBlockVisible(modelView.Grid[x, y]);
            }
        }

        private float timeDelta;
        public void HandleCollisions(float updatePeriod)
        {
            player.ResolveCollision();

            foreach(Obstacle obstacle in obstacles.ToList())
            {
                obstacle.ResolveCollision();
            }

            foreach (Enemy enemy in enemies.ToList())
            {
                enemy.ResolveCollision();               
            }

            foreach (Bomb bomb in bombs.ToList())
            {
                bomb.ResolveCollision();

                if (bomb.State == BombState.EXPLODE)
                {
                    timeDelta += updatePeriod;
                    if (timeDelta > 0.3f)
                    {
                        bombs.Remove(bomb);
                        int x = GridUtil.TransformPositionRelative(bomb.Bounds.CenterX, 0, modelView.gridSize);
                        int y = GridUtil.TransformPositionRelative(bomb.Bounds.CenterY, 0, modelView.gridSize);
                        bomb.Bounds.RemoveReferencedObject(modelView.Grid[x,y]);
                        timeDelta = 0;
                    }
                }
            }
        }

        private void plantBomb(object sender, EventArgs args)
        {
            if(availableAmountOfBombs>0)
            {
                Player player = (Player)sender;
                Bomb bomb = new BombBigRadius(new Vector2(player.Bounds.MinX + player.Bounds.SizeX * 0.25f,
                    player.Bounds.MinY + player.Bounds.SizeY * 0.25f),
                    player.Bounds.SizeX);
                bombs.Add(bomb);
                Block block = AddComponentToGrid(bomb.Bounds, BlockType.BOMB);
                modelView.RegisterComponent(bomb, block);
                availableAmountOfBombs--;
            }
        }

        private Block AddComponentToGrid(Box2D component, BlockType type)
        {
            Block block = new Block(type, "char", new Box2D(0, 0, 0, 0), component.SizeX, component.MinX, component.MinY);
            modelView.AddComponentToGrid(block);
            return block;
        }

        private void Lost()
        {
            isGameOver = true;
            onLost?.Invoke(this, null);
        }

        private void Lost(object sender, EventArgs args)
        {
            Block block = (Block)sender;
            if (block.Bounds.Intersects(player.Bounds))
            {
                Lost();
            }
        }

        private void Win()
        {
            isGameOver = true;
        }

        private void Win(object sender, EventArgs args)
        {
            Block block = (Block)sender;
            if (block.Bounds.Intersects(player.Bounds))
            {
                if (portal.IsVisible)
                {
                    Win();
                }
            }
        }
        private void GenerateImplementation(List<Block>[,] grid)
        {
            this.enemies = new List<Enemy>();
            this.obstacles = new List<Obstacle>();
            this.bombs = new List<Bomb>();
            for (int y = 0; y < ModelView.Instance.gridSize; y++)
            {
                for (int x = 0; x < ModelView.Instance.gridSize; x++)
                {
                    foreach (Block block in grid[x, y])
                    {
                        Generate(block);
                    }
                }
            }

            if (player == null)
            {
                throw new Exception("No existing Player in Level");
            }
            if (portal == null)
            {
                throw new Exception("No existing Portal in Level");
            }
        }

        private void Generate(Block block)
        {
            switch (block.Type)
            {
                case BlockType.PLAYER:
                    player = new Player(block.Bounds);
                    player.onPlantBomb += (sender, args) => plantBomb(sender, args);
                    player.onEnemyCollision += (sender, args) => Lost(sender, args);
                    player.onBombCollision += (sender, args) => HandlePlayerBombCollision(sender, args);
                    player.onPortalCollision += (sender, args) => Win(sender, args);
                    player.onItemCollision += (sender, args) => AddItem(sender, args);
                    player.objectWalks += (sender, args) => walkAnimation(block, args);
                    modelView.RegisterComponent(player, block);
                    Camera camera = Camera.Instance;
                    camera.FocusedElement = player.Bounds;
                    break;
                case BlockType.OBSTACLE:
                    Obstacle obstacle = new Obstacle(block.Bounds);
                    obstacle.onBombCollision += (sender, args) => HandleObstacleBombCollision(sender, args);
                    obstacles.Add(obstacle);
                    modelView.RegisterComponent(obstacle, block);
                    break;
                case BlockType.PORTAL:
                    portal = new Portal(block.Bounds);
                    break;
                case BlockType.ENEMY:
                    Enemy enemy = new Enemy(block.Bounds);
                    enemy.onBombCollision += (sender, args) => HandleEnemyBombCollision(sender, args);
                    enemy.objectWalks += (sender, args) => walkAnimation(block, args);
                    enemies.Add(enemy);                
                    modelView.RegisterComponent(enemy, block);
                    break;
            }
        }

        private void AddItem(object sender, EventArgs args)
        {
            availableAmountOfBombs++;
        }

        private void HandlePlayerBombCollision(object sender, EventArgs args)
        {
            foreach (Bomb bomb in bombs)
            {
                if (bomb.State == BombState.EXPLODE)
                {
                    Lost();
                }
            }
        }

        private void HandleObstacleBombCollision(object sender, EventArgs args)
        {
            GameObject block = (GameObject)sender;
            foreach (Obstacle obstacle in obstacles.ToArray())
            {
                if (obstacle.Bounds.Equals(block.Bounds))
                {
                    foreach (Bomb bomb in bombs)
                    {
                        if (bomb.State == BombState.EXPLODE)
                        {
                            obstacles.Remove(obstacle);
                            int x = GridUtil.TransformPositionRelative(obstacle.Bounds.CenterX, 0, modelView.gridSize);
                            int y = GridUtil.TransformPositionRelative(obstacle.Bounds.CenterY, 0, modelView.gridSize);
                            obstacle.Bounds.RemoveReferencedObject(modelView.Grid[x, y]);
                        }
                    }
                }
            }
        }

        private void HandleEnemyBombCollision(object sender, EventArgs args)
        {
            Enemy enemy = (Enemy)sender;
            if (enemy != null)
            {
                foreach(Bomb bomb in bombs)
                {
                    if(bomb.State == BombState.EXPLODE)
                    {
                        enemies.Remove((Enemy)enemy);
                        int x = GridUtil.TransformPositionRelative(enemy.Bounds.CenterX, 0, modelView.gridSize);
                        int y = GridUtil.TransformPositionRelative(enemy.Bounds.CenterY, 0, modelView.gridSize);
                        enemy.Bounds.RemoveReferencedObject(modelView.Grid[x, y]);
                        onEnemyDestroy?.Invoke(this, null);
                    }
                }
            }
        }

        private void walkAnimation(Block block, int walkState)
        {
            if (walkState == 1 || walkState == 2)
            {
                block.WalkingState = walkState;
            }
        }
    }
}
