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
using bombs_away.ui.elements.ground;
using System.IO;

namespace bombs_away
{
    class LevelLoader
    {
        private float squareSize;
        public Level Load()
        {
            int levelSize = CalculateAmountOfBlocksInXDirection();
            squareSize = CalculateSquareSize(levelSize);

            Block[,] grid = new Block[levelSize, levelSize];
            List<Block> interactiveObjects = new List<Block>();
            StreamReader reader = new StreamReader("..\\..\\resources\\game\\map\\Primitive.txt");
		    
            for (int y = levelSize-1; y >= 0; y--)
            {
                string line = reader.ReadLine();
                for (int x = levelSize-1; x >= 0; x--)
                {
                    if (isComponentStatic(line[x]))
                    {
                        grid[x, y] = LoadComponent(x, y, line[x]);
                    }
                    else
                    {
                        interactiveObjects.Add(LoadComponent(x, y, line[x]));
                        grid[x, y] = LoadComponent(x, y, '&');
                    }
                }
            }

            return new Level(grid, interactiveObjects);
        }

        private bool isComponentStatic(char type)
        {
            return type.Equals('&') || type.Equals('_');
        }

        private Block LoadComponent(int gridX, int gridY, char type)
        {
            float x = TransformPositionRelative(gridX);
            float y = TransformPositionRelative(gridY);
            switch (type)
            {
                case '&': return new Block(BlockType.EMPTY, squareSize, x, y, false);
                case 'E': return new Block(BlockType.ENEMY, squareSize, x, y);
                case 'p': return new Block(BlockType.PLAYER, squareSize, x, y);
                case 'P': return new Block(BlockType.PORTAL, squareSize, x, y, false);
                case '_': return new Block(BlockType.GROUND, squareSize, x, y);
            }
            return new Block(BlockType.EMPTY, squareSize, x, y, false);
        }

        private float TransformPositionRelative(int gridPosition)
        {
            return (float)gridPosition / (float)StaticValues.GRIDSIZE;
        }

        private float CalculateSquareSize(float levelSize)
        {
            return 1/levelSize;
        }

        private int CalculateAmountOfBlocksInXDirection()
        {
            return 10;
        }
    }
}