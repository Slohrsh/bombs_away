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
using TiledSharp;
using Zenseless.Geometry;
using Zenseless.HLGL;
using static bombs_away.TiledObjectCodes;

namespace bombs_away
{
    class LevelLoader
    {
        private float squareSize;
        private XmlDocument doc;
        private IList<SpriteSheet> spriteSheetList;
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
            TmxMap map = new TmxMap("../../resources/game/map/BasicMap.tmx");
            TextureLoader textureLoader = new TextureLoader();
            IList<ITexture> textureList = textureLoader.LoadContent();
            IList<SpriteSheet> spriteSheetList = loadSpriteSheets(textureList);
            IList<TmxLayer> layerList = map.Layers.ToList();

            Block[,] grid = new Block[map.Width, map.Height];
            List<Block> interactiveObjects = new List<Block>();

            foreach (TmxLayer layer in layerList)
            {
                IList<TmxLayerTile> tileList = layer.Tiles;

                foreach (TmxLayerTile tile in tileList)
                {
                    if (isComponentStatic(tile.Gid.ToString()))
                    {
                        grid[tile.X, tile.Y] = LoadComponent(tile.X, tile.Y, tile.Gid.ToString(), layer.Name, spriteSheetList);
                    }
                    else
                    {
                        interactiveObjects.Add(LoadComponent(tile.X, tile.Y, tile.Gid.ToString(), layer.Name, spriteSheetList));
                        grid[tile.X, tile.Y] = LoadComponent(tile.X, tile.Y, "0", layer.Name, spriteSheetList);
                    }
                }
            }

            //doc = new XmlDocument();
            //doc.Load("../../resources/game/map/BasicMap.tmx");

            //squareSize = CalculateSquareSize(map.Width);

            //Block[,] grid = new Block[map.Width, map.Height];
            //List<Block> interactiveObjects = new List<Block>();
            //Stream stream = GenerateStreamFromString(getNodeValue("/map/layer/data"));
            //TextFieldParser parser = new TextFieldParser(stream);
            //parser.TextFieldType = FieldType.Delimited;
            //parser.SetDelimiters(",");

            //for (int y = map.Height - 1; y >= 0; y--)
            //{
            //    //Console.WriteLine(y);
            //    string[] line = parser.ReadFields();

            //    for (int x = map.Width - 1; x >= 0; x--)
            //    {
            //        if (isComponentStatic(line[x]))
            //        {
            //            grid[x, y] = LoadComponent(x, y, line[x], spriteSheetList);
            //        }
            //        else
            //        {
            //            interactiveObjects.Add(LoadComponent(x, y, line[x], spriteSheetList));
            //            grid[x, y] = LoadComponent(x, y, "0", spriteSheetList);
            //        }
            //    }
            //}

            //return new Level(grid, interactiveObjects);
            return new Level(grid, interactiveObjects);
        }

        private IList<SpriteSheet> loadSpriteSheets(IList<ITexture> textureList)
        {
            IList<SpriteSheet> spriteSheetList = new List<SpriteSheet>();
            foreach (ITexture texture in textureList)
            {
                spriteSheetList.Add(new SpriteSheet(texture, 10, 6));
            }
            return spriteSheetList;
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
            return type.Equals(TiledObjectCodes.mapCodes.EMPTY_SPACE) || type.Equals(TiledObjectCodes.mapCodes.DIRT)
                   || type.Equals(TiledObjectCodes.mapCodes.GROUND_WITH_GRASS);
        }

        private Block LoadComponent(int gridX, int gridY, String type, String layerName, IList<SpriteSheet> spriteSheetList)
        {
            SpriteSheet charactersSpriteSheet = spriteSheetList[0];
            SpriteSheet mapSpriteSheet = spriteSheetList[1];

            float x = TransformPositionRelative(gridX);
            float y = TransformPositionRelative(gridY);

            uint typeUint = stringToUint(type) - 1;

            if (layerName == "Map")
            {
                switch (stringToInt(type))
                {
                    case (int)TiledObjectCodes.mapCodes.EMPTY_SPACE:
                        return new Block(BlockType.EMPTY, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y, false);
                    case (int)TiledObjectCodes.mapCodes.GROUND_WITH_GRASS:
                        return new Block(BlockType.GROUND, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    case (int)TiledObjectCodes.mapCodes.DIRT:
                        return new Block(BlockType.DIRT, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    default:
                        return new Block(BlockType.EMPTY, null, squareSize, x, y, false);
                }
            }

            if (layerName == "Characters")
            {
                switch (stringToInt(type))
                {
                    case (int)TiledObjectCodes.characterCodes.ENEMY_VAMPIRE:
                        return new Block(BlockType.ENEMY, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    case (int)TiledObjectCodes.characterCodes.PLAYER:
                        return new Block(BlockType.PLAYER, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    case (int)TiledObjectCodes.characterCodes.PORTAL:
                        return new Block(BlockType.PORTAL, mapSpriteSheet.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    default:
                        return new Block(BlockType.EMPTY, null, squareSize, x, y, false);
                }

            }
            return new Block(BlockType.EMPTY, null, squareSize, x, y, false);
        }


        private static uint stringToUint(string type)
            {
                int typeInt = Int32.Parse(type);
                return (uint)typeInt;
            }

            private static int stringToInt(string type)
            {
                return Int32.Parse(type);
            }

            private float TransformPositionRelative(int gridPosition)
            {
                return (float)gridPosition / (float)StaticValues.GRIDSIZE;
            }

            private float CalculateSquareSize(float levelSize)
            {
                return 1 / levelSize;
            }
        }
    }