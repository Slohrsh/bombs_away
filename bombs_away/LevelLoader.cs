using System;
using System.Collections.Generic;
using bombs_away.game;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;
using TiledSharp;
using Zenseless.HLGL;


namespace bombs_away
{
    class LevelLoader
    {
        private float squareSize;
        private XmlDocument doc;
        private SpriteSheet spriteSheetMap;
        private SpriteSheet spriteSheetChar;

        private ModelView modelView = ModelView.Instance;
        
        

        public Level Load()
        {
            TmxMap map = new TmxMap("../../resources/game/map/BasicMap.tmx");
            TextureLoader textureLoader = new TextureLoader();
            IList<ITexture> textureList = textureLoader.LoadContent();          
            spriteSheetMap = new SpriteSheet(textureList[0], 10, 6);
            spriteSheetChar = new SpriteSheet(textureList[1], 8, 26);


            modelView.gridSize = map.Height;
            
            doc = new XmlDocument();
            doc.Load("../../resources/game/map/BasicMap.tmx");

            squareSize = CalculateSquareSize(map.Width);

            List<Block>[,] grid = new List<Block>[map.Width, map.Height];
            List<Block> interactiveObjects = new List<Block>();
            Stream stream = GenerateStreamFromString(getNodeValue("/map/layer/data"));
            TextFieldParser parser = new TextFieldParser(stream);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            for (int y = map.Height - 1; y >= 0; y--)
            {
                //Console.WriteLine(y);
                string[] line = parser.ReadFields();

                for (int x = map.Width - 1; x >= 0; x--)
                {
                        grid[x, y] = LoadComponent(x, y, line[x]);
                }
            }

            return new Level(grid);
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
            return type.Equals(TiledObjectCodes.EMPTY_SPACE) || type.Equals(TiledObjectCodes.DIRT)
                   || type.Equals(TiledObjectCodes.GROUND_WITH_GRASS);
        }

        private List<Block> LoadComponent(int gridX, int gridY, String type)
        {
            float x = TransformPositionRelative(gridX);
            float y = TransformPositionRelative(gridY);
            List<Block> blocks = new List<Block>();
            Block block = null;
     
            uint typeUint = stringToUint(type)-1;
            switch (type)
            {
                case TiledObjectCodes.EMPTY_SPACE:
                    block = new Block(BlockType.EMPTY, "map",
                    spriteSheetMap.CalcSpriteTexCoords(typeUint), squareSize, x,  y, false);
                    break;
                case TiledObjectCodes.ENEMY:
                    block = new Block(BlockType.ENEMY, "char",
                    spriteSheetChar.CalcSpriteTexCoords(typeUint), squareSize / 1.1f, x, y);
                    break;
                case TiledObjectCodes.PLAYER:
                    block = new Block(BlockType.PLAYER, "char",
                    spriteSheetChar.CalcSpriteTexCoords(typeUint), squareSize / 1.1f, x, y);
                    break;
                case TiledObjectCodes.PORTAL:
                    block = new Block(BlockType.PORTAL, "char",
                    spriteSheetChar.CalcSpriteTexCoords(typeUint), squareSize, x, y, false);
                    break;
                case TiledObjectCodes.GROUND_WITH_GRASS:
                    block = new Block(BlockType.GROUND, "map",
                    spriteSheetMap.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    break;
                case TiledObjectCodes.DIRT:
                    block = new Block(BlockType.GROUND, "map",
                    spriteSheetMap.CalcSpriteTexCoords(typeUint),squareSize, x, y);
                    break;
                case TiledObjectCodes.OBSTACLE:
                    block = new Block(BlockType.OBSTACLE, "map",
                    spriteSheetMap.CalcSpriteTexCoords(typeUint), squareSize, x, y);
                    break;
            }
            if(block == null)
            {
                block = new Block(BlockType.EMPTY, "map", null, squareSize, x, y, false);
            }
            blocks.Add(block);
            return blocks;
        }

        private static uint stringToUint(string type)
        {
            int typeInt = Int32.Parse(type);
            uint typeUint = (uint) typeInt;
            return typeUint;
        }

        private float TransformPositionRelative(int gridPosition)
        {
            return (float) gridPosition / (float)modelView.gridSize;
        }

        private float CalculateSquareSize(float levelSize)
        {
            return 1 / levelSize;
        }
    }
}