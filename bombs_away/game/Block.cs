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
        private Box2D component;
        private Color4 color;
        private bool isVisible = true;

        public BlockType Type { get { return type; } }
        public Box2D Component { get { return component; } set { component = value; } }
        public bool IsVisible { get { return isVisible; } set { isVisible = value; } }
        public Color4 Color { get { return color; } }

        public Block(BlockType type, float size, float positionX, float positionY)
        {
            this.type = type;
            this.component = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
            color = DistinguishColor(type);
        }
        public Block(BlockType type, float size, float positionX, float positionY, bool isVisible)
        {
            this.type = type;
            this.component = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
            this.isVisible = isVisible;
            color = DistinguishColor(type);
        }

        private Color4 DistinguishColor(BlockType type)
        {
            switch(type)
            {
                case BlockType.ENEMY: return Color4.Red;
                case BlockType.GROUND: return Color4.White;
                case BlockType.PLAYER: return Color4.Green;
                case BlockType.OBSTACLE: return Color4.White;
                case BlockType.PORTAL: return Color4.Blue;
                case BlockType.BOMB: return Color4.Brown;
            }
            return Color4.Black;
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
