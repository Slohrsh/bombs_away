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
        private static Camera instance;
        public Box2D FocusedElement { private get;  set; }
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera(new Box2D(0, 0, 0.5f, 0.5f));
                }
                return instance;
            }
        }

        private Camera(Box2D bounds)
        {
            Bounds = bounds;
        }

        public void SetResolution(int width, int heigth)
        {
            float relativeY = (((float)heigth / (float)width) * (10f * Bounds.SizeX)) / 10;
            Bounds.SizeY = relativeY;
        }

        public void beginDraw()
        {
            //nach Kamera aktivierung dieser Ablauf
            GL.PushMatrix(); //speichert die aktuelle Matrix
            GL.LoadIdentity();
            GL.Translate(0,0,0.1);
            GL.Ortho(Bounds.MinX, Bounds.MaxX, Bounds.MinY, Bounds.MaxY, 0.1 , 100.0); // danach alles rendern / auch Game Objects
            float borderX = FocusedElement.CenterX - Bounds.SizeX / 2;
            if (FocusedElement.CenterX - Bounds.SizeX / 2 > 0 
                && FocusedElement.CenterX + Bounds.SizeX / 2 < 1)
            {
                Bounds.CenterX = FocusedElement.CenterX;
            }
            if (FocusedElement.CenterY - Bounds.SizeY / 2 > 0
                && FocusedElement.CenterY + Bounds.SizeY / 2 < 1)
            {
                Bounds.CenterY = FocusedElement.CenterY;
            }
        }

        public void endDraw()
        {
            GL.PopMatrix(); // den alten State der Matrix wiederherstellen
        }
    }
}
