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
        private Level level;

        public GameView(Level level)
        {
            this.level = level;
        }

        internal void DrawScreen()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //Wichtig für Kamera:
            //GL.Translate()
            //GL.Scale()
            if (level.obstacles != null)
            {
                foreach (Obstacle obstacle in level.obstacles)
                {
                    Draw(obstacle.Component, Color4.White);
                }
            }
            if (level.enemies != null)
            {
                foreach (Enemy enemy in level.enemies)
                {
                    Draw(enemy.Component, Color4.Red);
                }
            }
            if (level.bombs != null)
            {
                foreach (Bomb bomb in level.bombs)
                {
                    Draw(bomb.Component, Color4.Pink);
                }
            }
            Draw(level.player.Component, Color4.Green);
        }

        private void Draw(Box2D component, Color4 color)
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
