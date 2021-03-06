﻿using bombs_away.game;
using bombs_away.ui.enums;
using bombs_away.ui.physics;
using bombs_away.ui.zenseless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.ui.interactive
{
    class Moveable : GameObject
    {
        public event EventHandler<int> objectWalks;    
        
        ModelView modelView = ModelView.Instance;
        protected virtual void MoveX(float value)
        {
            Bounds.MinX += value * 5 / modelView.gridSize;
        }

        protected virtual void MoveY(float value)
        {
            Bounds.MinY += value / modelView.gridSize;
        }

        public void invoke(int directionValue)
        {
            objectWalks?.Invoke(this, directionValue);
        }
    }
}
