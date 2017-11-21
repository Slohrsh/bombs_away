using OpenTK.Graphics.OpenGL;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using OpenTK.Graphics;
using bombs_away.game;
using Zenseless.Geometry;

namespace bombs_away.controller
{
    class GameView
    {
        private ModelView modelView;

        public GameView(ModelView modelView)
        {
            this.modelView = modelView;
        }

        internal void DrawScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //Wichtig für Kamera:
            //GL.Translate()
            //GL.Scale()
            if (modelView.Obstacles != null)
            {
                foreach (Obstacle obstacle in modelView.Obstacles)
                {
                    Draw(obstacle.Component, Color4.White, obstacle.IsVisible);
                }
            }
            if (modelView.Enemies != null)
            {
                foreach (Enemy enemy in modelView.Enemies)
                {
                    Draw(enemy.Component, Color4.Red, enemy.IsVisible);
                }
            }
            if (modelView.Bombs != null)
            {
                foreach (Bomb bomb in modelView.Bombs)
                {
                    Draw(bomb.Component, Color4.Pink, bomb.IsVisible);
                }
            }
            Draw(modelView.Player.Component, Color4.Green, modelView.Player.IsVisible);
            Draw(modelView.Portal.Component, Color4.Blue, modelView.Portal.IsVisible);
        }

        private void Draw(Box2D component, Color4 color, bool isVisible)
        {
            if (isVisible)
            {
                if (component != null)
                {
                    GL.Begin(PrimitiveType.LineLoop);
                    GL.Color4(color);
                    GL.Vertex2(component.MinX, component.MinY);
                    GL.Vertex2(component.MaxX, component.MinY);
                    GL.Vertex2(component.MaxX, component.MaxY);
                    GL.Vertex2(component.MinX, component.MaxY);
                    GL.End();
                }
            }
        }
    }
}
