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
        private string textureType;
        private Box2D textureCoordinates;
        private bool isVisible = true;
        public Box2D Bounds { get; set; }

        public BlockType Type
        {
            get { return type; }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public string TextureType
        {
            get { return textureType; }
        }

        public Box2D TextureCoordinates
        {
            get { return textureCoordinates; }
            set { textureCoordinates = value; }
        }

        public Box2D idleCoordinates;

        public List<Box2D> animationCoordinates = new List<Box2D>();

        private int walkingState = 0;

        public int WalkingState
        {
            get { return walkingState; }
            set
            {
                animationCoordinates.Clear();
                if (value == 1)
                {
                    animationCoordinates.Add(new Box2D(idleCoordinates.MinX + 0.1f,
                        idleCoordinates.MinY + 0.1f,
                        idleCoordinates.SizeX,
                        idleCoordinates.SizeY));
                    animationCoordinates.Add(new Box2D(idleCoordinates.MinX + 0.2f,
                        idleCoordinates.MinY + 0.2f,
                        idleCoordinates.SizeX,
                        idleCoordinates.SizeY));
                }
                else
                {
                    animationCoordinates.Add(new Box2D(idleCoordinates.MinX + 0.1f,
                        idleCoordinates.MinY + 0.3f,
                        idleCoordinates.SizeX,
                        idleCoordinates.SizeY));
                    animationCoordinates.Add(new Box2D(textureCoordinates.MinX + 0.2f,
                        idleCoordinates.MinY + 0.4f,
                        idleCoordinates.SizeX,
                        idleCoordinates.SizeY));
                }
                walkingState = value;
            }
        }

        public Block(BlockType type, string textureType, Box2D textureCoordinates, float size, float positionX,
            float positionY)
        {
            this.type = type;
            this.textureType = textureType;
            this.textureCoordinates = textureCoordinates;
            this.idleCoordinates = this.textureCoordinates;
            this.Bounds = Box2DFactory.CreateSquare(new Vector2(positionX, positionY), size);
        }

        public Block(BlockType type, string textureType, Box2D textureCoordinates, float size, float positionX,
            float positionY, bool isVisible)
        {
            this.type = type;
            this.textureType = textureType;
            this.textureCoordinates = textureCoordinates;
            this.idleCoordinates = this.textureCoordinates;
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
        BOMB,
        ITEM
    }
}