using bombs_away.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Application;

namespace bombs_away
{
    class Main
    {
        [STAThread]
        private static void Main()
        {
            var window = new ExampleWindow();
            var controller = new Controller();
            window.Render += controller.Render;
            window.Update += controller.Update;
            window.Run();
        }
    }
}
