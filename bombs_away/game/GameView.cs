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
using bombs_away.ui.elements.obstacle;

namespace bombs_away.controller
{
    class GameView
    {

        internal void DrawScreen(Player player, List<Enemy> enemies, List<Obstacle> obstacles, List<Bomb> bombs)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //Wichtig für Kamera:
            //GL.Translate()
            //GL.Scale()
            if (obstacles != null)
            {
                foreach (Obstacle obstacle in obstacles)
                {
                    obstacle.Draw();
                }
            }
            if (enemies != null)
            {
                foreach (Enemy enemie in enemies)
                {
                    enemie.Draw();
                }
            }
            if (bombs != null)
            {
                foreach (Bomb bomb in bombs)
                {
                    bomb.Draw();
                }
            }
            player.Draw();
        }
    }
}
