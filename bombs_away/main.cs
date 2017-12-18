using bombs_away.controller;
using bombs_away.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Application;

namespace bombs_away
{
    class main
    {
        [STAThread]
        private static void Main()
        {
            var window = new ExampleWindow();
            window.GameWindow.WindowState = OpenTK.WindowState.Fullscreen;
            var controller = new Controller();
            window.Render += controller.Render;
            window.Update += (t) => controller.Update(t*1f);
            window.Resize += (width, height) => controller.onResize(width, height);
            window.Run();
        }


    }
}
