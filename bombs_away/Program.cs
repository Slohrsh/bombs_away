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
		//private Box2D obstacle = new Box2D(-0.2f, 1, 0.4f, 0.2f);
		private Box2D player = new Box2D(0.0f, -0.95f, 0.2f, 0.2f);
        private const String JUMPING_UP = "JUMPING_UP";
        private const String JUMPING_DOWN = "JUMPING_DOWN";
        private const String WALKING = "WALKING";
        private String state = WALKING;

		private void Update(float updatePeriod)
		{
			if(Keyboard.GetState()[Key.Left] && player.MinX > -1)
			{
				player.MinX -= updatePeriod;
			}
			else if (Keyboard.GetState()[Key.Right] && player.MaxX < 1)
			{
				player.MinX += updatePeriod;
			}
            if(Keyboard.GetState()[Key.Space] && state == WALKING)
			{
				state = JUMPING_UP;
			}



            if (state == JUMPING_UP)
            {
                player.MinY += updatePeriod;
                if(player.MinY > -0.8) 
                { 
                    state = JUMPING_DOWN; 
                }
            }else if (state == JUMPING_DOWN)
            {
                player.MinY -= updatePeriod;
                if(player.MinY < -1) 
                { 
                    state = WALKING; 
                }
            }

		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			//GL.Color3(Color.CornflowerBlue);
			DrawComplex(player);
			//DrawComplex(obstacle);

			GL.LineWidth(2.0f);
			//GL.Color3(Color.YellowGreen);
			DrawBoxOutline(player);
			//DrawBoxOutline(obstacle);
		}

		private void DrawBoxOutline(Box2D rect)
		{
			GL.Begin(PrimitiveType.LineLoop);
			GL.Vertex2(rect.MinX, rect.MinY);
			GL.Vertex2(rect.MaxX, rect.MinY);
			GL.Vertex2(rect.MaxX, rect.MaxY);
			GL.Vertex2(rect.MinX, rect.MaxY);
			GL.End();
		}

		private void DrawComplex(Box2D rect)
		{
			var xQuarter = rect.MinX + rect.SizeX * 0.25f;
			var x3Quarter = rect.MinX + rect.SizeX * 0.75f;
			var yThird = rect.MinY + rect.SizeY * 0.33f;
			var y2Third = rect.MinY + rect.SizeY * 0.66f;
			GL.Begin(PrimitiveType.Polygon);
			GL.Vertex2(rect.CenterX, rect.MaxY);
			GL.Vertex2(x3Quarter, y2Third);
			GL.Vertex2(rect.MaxX, rect.CenterY);
			GL.Vertex2(x3Quarter, yThird);
			GL.Vertex2(rect.MaxX, rect.MinY);
			GL.Vertex2(rect.CenterX, yThird);
			GL.Vertex2(rect.MinX, rect.MinY);
			GL.Vertex2(xQuarter, yThird);
			GL.Vertex2(rect.MinX, rect.CenterY);
			GL.Vertex2(xQuarter, y2Third);
			GL.End();
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