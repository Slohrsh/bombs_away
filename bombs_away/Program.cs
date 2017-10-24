using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new GameWindow();
            window.RenderFrame += Window_RenderFrame;
            window.RenderFrame += (s, e) => window.SwapBuffers();
            window.Run();
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
