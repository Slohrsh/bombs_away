using bombs_away.ui.elements;
using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.player;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.controller
{
    class GameLogic
    {
        private Player player;
        private List<Enemy> enemies;
        private List<Obstacle> obstacles;
        private List<Bomb> bombs;

        public Player Player { get { return player; }}
        public List<Enemy> Enemies { get { return enemies; } }
        public List<Obstacle> Obstacles { get { return obstacles; } }
        public List<Bomb> Bombs { get { return bombs; } }

        public GameLogic(Player player, List<Enemy> enemies, List<Obstacle> obstacles, List<Bomb> bombs)
        {
            this.player = player;
            this.enemies = enemies;
            this.obstacles = obstacles;
            this.bombs = bombs;
        }

        public void Update(float updatePeriod, float axisLeftRight)
        {
            if (Keyboard.GetState()[Key.Left])
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
    }
}
