using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using bombs_away.ui.enums;
using bombs_away.ui.exceptions;

namespace bombs_away.ui.openGL
{
    class Drawable
    {

        protected Box2D component;

        public Box2D Component { get { return component; } }

        public void Draw()
        {
            if (component != null)
            {
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(component.MinX, component.MinY);
                GL.Vertex2(component.MaxX, component.MinY);
                GL.Vertex2(component.MaxX, component.MaxY);
                GL.Vertex2(component.MinX, component.MaxY);
                GL.End();
            }
            else
            {
                throw new ComponentNotSetException();
            }
        }

        public bool Intersects(Drawable rectangle)
        {
            if (rectangle == null)
            {
                return false;
            }
            return component.Intersects(rectangle.Component);
        }
    }
}
