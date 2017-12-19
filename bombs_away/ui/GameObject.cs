using bombs_away.game;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui
{
    public class GameObject 
    {
       
        public Vector2 Position { get { return new Vector2(Bounds.MinX, Bounds.MinY); } }
        protected bool isVisible;

        public bool IsVisible { get { return isVisible; } }

        public Box2D Bounds { get; protected set; }
        public Box2D Hitbox
        {
            get
            {
                return new Box2D(
                    Bounds.MinX + Bounds.SizeX * 0.2f,
                    Bounds.MinY,
                    Bounds.SizeX * 0.6f,
                    Bounds.SizeY);
            }
        }

        public void setVisible()
        {
            isVisible = true;
        }

        public bool Intersects(GameObject rectangle)
        {
            if (rectangle == null)
            {
                return false;
            }
            return Bounds.Intersects(rectangle.Bounds);
        }
    }
}
