using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;
using bombs_away.ui.enums;

namespace bombs_away.ui
{
    class Drawable
    {

        protected Box2D component;

        public Box2D Component { get; }
        private const String JUMPING_UP = "JUMPING_UP";
        private const String JUMPING_DOWN = "JUMPING_DOWN";
        private const String WALKING = "WALKING";
        private String state = WALKING;

        public void Draw()
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

        public bool Intersects(Drawable rectangle)
        {
            return component.Intersects(rectangle.Component);
        }

        public void Execute(Movement movement, float value)
        {
            switch(movement)
            {
                case Movement.UP:
                    ShiftUp(value);
                    break;
                case Movement.DOWN:
                    ShiftDown(value);
                    break;
                case Movement.LEFT:
                    ShiftLeft(value);
                    break;
                case Movement.RIGTH:
                    ShiftRight(value);
                    break;
                case Movement.JUMP:
                    Jump(value);
                    break;
            }
        }

        public void ShiftLeft(float value)
        {
            if (component.MinX > -1)
            {
                component.MinX -= value;
            }
        }

        public void ShiftRight(float value)
        {
            if (component.MaxX < 1)
            {
                component.MinX += value;
            }
        }

        public void ShiftUp(float value)
        {
            if (component.MaxY < 1)
            {
                component.MinY += value;
            }
        }

        public void ShiftDown(float value)
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
                ShiftUp(value);
            }
            else if (state == JUMPING_DOWN)
            {
                ShiftDown(value);
            }
        }
    }
}
