using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace bombs_away.game
{
    class Camera : GameObject
    {
        public Camera(Box2D bounds)
        {
            Bounds = bounds;
        }

        public void beginDraw()
        {
            //nach Kamera aktivierung dieser Ablauf
            GL.PushMatrix(); //speichert die aktuelle Matrix
            GL.LoadIdentity(); //
            GL.Translate(0,0,0.1);
            GL.Ortho(Bounds.MinX, Bounds.MaxX, Bounds.MinY, Bounds.MaxY, 0.1 , 100.0); // danach alles rendern / auch Game Objects
            
        }

        public void endDraw()
        {
            GL.PopMatrix(); // den alten State der Matrix wiederherstellen
        }

        internal void setCenter(Vector2 zero)
        {
            Bounds.CenterX = zero.X;
            Bounds.CenterY = zero.Y;
        }
    }
}
