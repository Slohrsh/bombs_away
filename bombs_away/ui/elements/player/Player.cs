﻿using bombs_away.ui.enums;
using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.player
{
    class Player : Movable
    {
        public Player(Vector2 position)
        {
            this.component = Box2DFactory.getSquare(position, 0.2f);
        }

    }
}
