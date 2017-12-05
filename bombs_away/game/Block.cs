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
        private Box2D component;
        private Box2D textureCoordinates;
        private bool isVisible = true;

        public Box2D Component { get { return component; } set { component = value; } }
        public bool IsVisible { get { return isVisible; } set { isVisible = value; } }
        public Box2D TextureCoordinates { get { return textureCoordinates; } }

        public Block(Box2D textureCoordinates, float size, float positionX, float positionY)
        {
            this.textureCoordinates = textureCoordinates;
            this.component = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
        }
        public Block(Box2D textureCoordinates, float size, float positionX, float positionY, bool isVisible)
        {
            this.textureCoordinates = textureCoordinates;
            this.component = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
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
