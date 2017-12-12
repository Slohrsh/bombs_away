using bombs_away.ui.zenseless;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.game
{
    public class Block
    {
        private BlockType type;
        private Box2D textureCoordinates;
        private bool isVisible = true;
        public Box2D Bounds { get; set; }

        public BlockType Type { get { return type; } }
        public bool IsVisible { get { return isVisible; } set { isVisible = value; } }
        public Box2D TextureCoordinates { get { return textureCoordinates; } }

        public Block(BlockType type, Box2D textureCoordinates, float size, float positionX, float positionY)
        {
            this.type = type;
            this.textureCoordinates = textureCoordinates;
            this.Bounds = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
        }
        public Block(BlockType type, Box2D textureCoordinates, float size, float positionX, float positionY, bool isVisible)
        {
            this.type = type;
            this.textureCoordinates = textureCoordinates;
            this.Bounds = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
            this.isVisible = isVisible;
        }
    }

    public enum BlockType
    {
        GROUND,
        OBSTACLE,
        PLAYER,
        PORTAL,
        ENEMY,
        EMPTY,
        BOMB
    }  
}
