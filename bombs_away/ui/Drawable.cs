using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace bombs_away.ui
{
    class Drawable
    {
        protected Box2D component;
        private const String JUMPING_UP = "JUMPING_UP";
        private const String JUMPING_DOWN = "JUMPING_DOWN";
        private const String WALKING = "WALKING";
        private String state = WALKING;

        public void draw()
        {
            if(component != null)
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

        public void shiftLeft(float value)
        {
            if (component.MinX > -1)
            {
                component.MinX -= value;
            }
        }

        public void shiftRight(float value)
        {
            if (component.MaxX < 1)
            {
                component.MinX += value;
            }
        }

        public void shiftUp(float value)
        {
            if (component.MaxY < 1)
            {
                component.MinY += value;
            }
        }

        public void shiftDown(float value)
        {
            if (component.MinY > -1)
            {
                component.MinY -= value;
            }
        }

        public void Jump(float value)
        {
            if (state == WALKING)
            {
                state = JUMPING_UP;
            }
            if (state == JUMPING_UP)
            {
                shiftUp(value);
            }
            else if (state == JUMPING_DOWN)
            {
                shiftDown(value);
            }
        }
    }
}
