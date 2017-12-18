using Zenseless.Application;
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
            view = new GameView();
            level.onLost += (sender, args) => {  };
            level.onThrowBomb += (sender, args) => {  };
            level.onEnemyDestroy += (sender, args) => { };
        }

        internal void onResize(int width, int heigth)
        {
            GL.Viewport(0, 0, width, heigth);
            Camera.Instance.SetResolution(width, heigth);
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