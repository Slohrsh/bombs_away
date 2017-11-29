using bombs_away.component;
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
        public Vector2 Position { get { return new Vector2(body.MinX, body.MinY); } }
        public bool IsVisible { get { return isVisible; } }
        public Box2D Body { get { return body; } }

        protected List<IComponent> components = new List<IComponent>();
        protected Box2D body;
        protected bool isVisible;

        public void setVisible()
        {
            isVisible = true;
        }

        /*protected T[] GetComponent<T>()
        {
            return components.ToArray<T>();
        }*/

        public virtual void Execute(float updatePeriod)
        {
            foreach (IComponent component in components)
            {
                component.Execute(updatePeriod);
            }
        }

        public bool Intersects(GameObject rectangle)
        {
            if (rectangle == null)
            {
                return false;
            }
            return body.Intersects(rectangle.body);
        }
    }
}
