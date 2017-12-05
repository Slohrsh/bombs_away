using bombs_away.controller;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using static bombs_away.TiledObjectCodes;

namespace bombs_away
{
    class LevelLoader
    {
        private float squareSize;
        private XmlDocument doc;
        private int mapWidth;

        private int mapHeight;
        /*public Level Load()
        {
            int levelSize = CalculateAmountOfBlocksInXDirection();
            squareSize = CalculateSquareSize(levelSize);

            Block[,] grid = new Block[levelSize, levelSize];
            List<Block> interactiveObjects = new List<Block>();
            StreamReader reader = new StreamReader("../../resources/game/map/Primitive.txt");
		    
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
        }*/

        public Level Load()
        {
            doc = new XmlDocument();
            doc.Load("../../resources/game/map/BasicMap.tmx");
            setMapDimensions();

            squareSize = CalculateSquareSize(mapWidth);

            Block[,] grid = new Block[mapWidth, mapHeight];
            List<Block> interactiveObjects = new List<Block>();
            Stream stream = GenerateStreamFromString(getNodeValue("/map/layer/data"));
            TextFieldParser parser = new TextFieldParser(stream);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");


            for (int y = mapHeight - 1; y >= 0; y--)
            {
                //Console.WriteLine(y);
                string[] line = parser.ReadFields();

                for (int x = mapWidth - 1; x >= 0; x--)
                {
                    if (isComponentStatic(line[x]))
                    {
                        grid[x, y] = LoadComponent(x, y, line[x]);
                    }
                    else
                    {
                        interactiveObjects.Add(LoadComponent(x, y, line[x]));
                        grid[x, y] = LoadComponent(x, y, "0");
                    }
                }
            }

            return new Level(grid, interactiveObjects);
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 1; //Curser auf 1 da die erste Zeile leer ist
            return stream;
        }


        private void setMapDimensions()
        {
            XmlNode node = doc.DocumentElement.SelectSingleNode("/map");
            mapWidth = getAttribute(node, "width");
            mapHeight = getAttribute(node, "height");
        }

        private int getAttribute(XmlNode node, String attributeName)
        {
            String mapWidthString = node.Attributes[attributeName]?.InnerText;
            return Int32.Parse(mapWidthString);
        }

        private String getNodeValue(String nodeName)
        {
            XmlNode node = doc.DocumentElement.SelectSingleNode(nodeName);
            return node?.InnerText;
        }

        private bool isComponentStatic(String type)
        {
            return type.Equals(TiledObjectCodes.EMPTY_SPACE) || type.Equals(TiledObjectCodes.DIRT)
                   || type.Equals(TiledObjectCodes.GROUND_WITH_GRASS);
        }

        private Block LoadComponent(int gridX, int gridY, String type)
        {
            float x = TransformPositionRelative(gridX);
            float y = TransformPositionRelative(gridY);
            if (type == "22")
            {
                Console.WriteLine(type);
            }
            switch (type)
            {
                case TiledObjectCodes.EMPTY_SPACE: return new Block(BlockType.EMPTY, squareSize, x, y, false);
                case TiledObjectCodes.ENEMY: return new Block(BlockType.ENEMY, squareSize, x, y);
                case TiledObjectCodes.PLAYER: return new Block(BlockType.PLAYER, squareSize, x, y);
                case TiledObjectCodes.PORTAL: return new Block(BlockType.PORTAL, squareSize, x, y, false);
                case TiledObjectCodes.GROUND_WITH_GRASS: return new Block(BlockType.GROUND, squareSize, x, y);
                case TiledObjectCodes.DIRT: return new Block(BlockType.GROUND, squareSize, x, y);
            }
            return new Block(BlockType.EMPTY, squareSize, x, y, false);
        }

        private float TransformPositionRelative(int gridPosition)
        {
            return (float) gridPosition / (float) StaticValues.GRIDSIZE;
        }

        private float CalculateSquareSize(float levelSize)
        {
            return 1 / levelSize;
        }
    }
}