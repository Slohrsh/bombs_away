using System;
using bombs_away.ui.zenseless;
using OpenTK;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.bombItem
{
    public class BombItem : GameObject
    {
        
        public BombItem(Vector2 position, float squareSize)
        {
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize);
        }
        public BombItem(Box2D component)
        {
            this.Bounds = new Box2D(component);
        }
    }
}