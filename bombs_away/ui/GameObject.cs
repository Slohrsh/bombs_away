using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui
{
    class GameObject 
    {
        protected Box2D component;
        protected bool isVisible = true;

        public bool IsVisible { get { return isVisible; } }

        public Box2D Component { get { return component; } }


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
