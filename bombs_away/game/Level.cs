using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.ground;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.elements.portal;
using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using bombs_away.util;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void plantBomb(object sender, EventArgs args)
        {
            Player player = (Player)sender;
            Bomb bomb = new BombBigRadius(new Vector2(player.Bounds.MinX, 
                player.Bounds.MinY),
                player.Bounds.SizeX);
            bombs.Add(bomb);
            Block block = AddComponentToGrid(bomb.Bounds, BlockType.BOMB);
            RegisterComponent(bomb.Bounds, block);
        }

        private void RegisterComponent(Box2D origin, Block reference)
        {
            reference.Bounds = origin;
        }

        private Block AddComponentToGrid(Box2D component, BlockType type)
        {
            Block block = new Block(type, new Box2D(0,0,0,0), component.SizeX, component.MinX, component.MinY);
            modelView.InteractiveObjects.Add(block);
            return block;
        }
        

        public Level(Block[,] grid, List<Block> interactiveObjects)
        {
            modelView.StaticGrid = grid;
            modelView.InteractiveObjects = interactiveObjects;
            GenerateImplementation(grid, interactiveObjects);
        }

        private void GenerateImplementation(Block[,] grid, List<Block> interactiveObjects)
        {
            this.enemies = new List<Enemy>();
            this.obstacles = new List<Obstacle>();
            this.bombs = new List<Bomb>();
            foreach(Block block in interactiveObjects)
            {
                Generate(block);
            }
            if(player == null)
            {
                throw new Exception("No existing Player in Level");
            }
            if(portal == null)
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
                    RegisterComponent(player.Bounds, block);
                    Camera camera = Camera.Instance;
                    camera.FocusedElement = player.Bounds;
                    break;
                case BlockType.OBSTACLE:
                    Obstacle obstacle = new Obstacle(block.Bounds);
                    obstacles.Add(obstacle);
                    RegisterComponent(obstacle.Bounds, block);
                    break;
                case BlockType.PORTAL:
                    portal = new Portal(block.Bounds);
                    break;
                case BlockType.ENEMY:
                    Enemy enemy = new Enemy(block.Bounds);
                    enemies.Add(enemy);
                    RegisterComponent(enemy.Bounds, block);
                    break;
            }
        }

        public void ExecuteAllElements(float updatePeriod)
        {
            //Console.WriteLine(player.Component.CenterY);
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
                portal.Bounds.SetReferencedBlockVisible(modelView.InteractiveObjects);
            }
        }

        private float timeDelta;
        public void HandleCollisions(float updatePeriod)
        {
            player.Grounded = false;

            if (portal.IsVisible)
            {
                if (player.Intersects(portal))
                {
                    Win();
                }
            }

            player.ResolveCollision();

            foreach (Enemy enemy in enemies.ToList())
            {
                enemy.ResolveCollision();

                if (enemy.Intersects(player))
                {
                    Console.WriteLine("Lost");
                    Lost();
                }

                foreach (Bomb bomb in bombs.ToList())
                {
                    if (bomb.State == BombState.EXPLODE)
                    {
                        if (bomb.Intersects(enemy))
                        {
                            enemies.Remove(enemy);
                            enemy.Bounds.RemoveReferencedObject(modelView.InteractiveObjects);
                            Console.WriteLine("Wuhuu! I've killed the Enemy!");
                            onEnemyDestroy?.Invoke(this, null);
                        }
                    }
                }
            }

            foreach (Bomb bomb in bombs.ToList())
            {
                bomb.ResolveCollision();

                if (bomb.State == BombState.EXPLODE)
                {
                    timeDelta += updatePeriod;
                    if (bomb.Intersects(player))
                    {
                        Console.WriteLine("Oh snap! I committed suicide!");
                        Lost();
                    }
                    if (timeDelta > 1)
                    {
                        bombs.Remove(bomb);
                        bomb.Bounds.RemoveReferencedObject(modelView.InteractiveObjects);
                        timeDelta = 0;
                    }
                }
            }
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
