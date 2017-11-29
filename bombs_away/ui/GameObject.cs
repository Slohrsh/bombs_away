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
        protected Box2D component;
        public Vector2 Position { get { return new Vector2(component.MinX, component.MinY); } }
        protected bool isVisible;

        public bool IsVisible { get { return isVisible; } }

        public Box2D Component { get { return component; } }

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
            return component.Intersects(rectangle.component);
        }
    }
}
