using bombs_away.ui.elements.bomb;
using bombs_away.ui.elements.enemy;
using bombs_away.ui.elements.obstacle;
using bombs_away.ui.elements.player;
using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using System;
using System.Linq;
using OpenTK;
using bombs_away.ui.interactive;
using bombs_away.ui;
using System.Collections.Generic;
using bombs_away.ui.elements.ground;

namespace bombs_away.game
{
    class GameLogic
    {
        private Level level;

        public GameLogic(Level level)
        {
            this.level = level;
        }

        public void Update(float updatePeriod)
        {
            if (!level.IsGameOver)
            {
                level.Execute(updatePeriod);
            }
        }
    }
}
