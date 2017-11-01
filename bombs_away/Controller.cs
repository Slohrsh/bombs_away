using Zenseless.Application;
using Zenseless.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace bombs_away
{
	class Controller
	{
        private Player player = new Player();

		private void Update(float updatePeriod)
		{
			if(Keyboard.GetState()[Key.Left])
			{
                player.shiftLeft(updatePeriod);
			}
			else if (Keyboard.GetState()[Key.Right])
			{
                player.shiftRight(updatePeriod);
			}
            if (Keyboard.GetState()[Key.Space])
			{
                player.Jump(updatePeriod);
			}
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
            player.draw();
		}

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