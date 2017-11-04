using Zenseless.Application;
using Zenseless.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using bombs_away.ui.elements.player;

namespace bombs_away.game
{
	class Controller
	{
        private Player player = new Player();

		public void Update(float updatePeriod)
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

		public void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
            player.draw();
		}
	}
}