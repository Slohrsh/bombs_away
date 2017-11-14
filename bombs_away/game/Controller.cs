﻿using Zenseless.Application;
using Zenseless.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using bombs_away.ui.elements.player;
using bombs_away.controller;
using bombs_away.ui.enums;
using System.Diagnostics;
using System.Collections.Generic;

namespace bombs_away.game
{
	class Controller
	{

        private GameLogic logic;
        private Level level;
        private GameView view;
        private LevelLoader builder;

        public Controller()
        {
            builder = new LevelLoader();
            level = builder.Load();
            logic = new GameLogic(level);
            view = new GameView(level);
            logic.onLost += (sender, args) => { };
            logic.onThrowBomb += (sender, args) => {  };
            logic.onEnemyDestroy += (sender, args) => { };
        }
		public void Update(float updatePeriod)
		{
            logic.Update(updatePeriod);
		}

        public void Render()
		{
            view.DrawScreen();
		}
	}
}