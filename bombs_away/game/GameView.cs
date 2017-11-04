using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using bombs_away.ui.elements.player;
using bombs_away.ui.elements;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;

namespace bombs_away.controller
{
    class GameView
    {

        internal void DrawScreen(Player player, List<Enemy> enemies, List<Obstacle> obstacles, List<Bomb> bombs)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            if(obstacles != null)
            {
                foreach (Obstacle obstacle in obstacles)
                {
                    obstacle.draw();
                }
            }
            if (enemies != null)
            {
                foreach (Enemy enemie in enemies)
                {
                    enemie.draw();
                }
            }
            if (bombs != null)
            {
                foreach (Bomb bomb in bombs)
                {
                    bomb.draw();
                }
            }
            player.draw();
        }
    }
}
