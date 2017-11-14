using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.game
{
    class Level
    {
        public Player player { get; set; }
        public List<Enemy> enemies { get; set; }
        public List<Obstacle> obstacles { get; set; }
        public List<Bomb> bombs { get; set; }

        public Level(Player player, List<Enemy> enemies, List<Obstacle> obstacles, List<Bomb> bombs)
        {
            this.player = player;
            this.enemies = enemies != null ? enemies : new List<Enemy>();
            this.obstacles = obstacles != null ? obstacles : new List<Obstacle>();
            this.bombs = bombs != null ? bombs : new List<Bomb>(); ;
        }
    }
}
