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
        private GameView view;
        private GameMapBuilder builder;

        private float lastUpdateTime = 0.0f;

        public Controller()
        {
            builder = new GameMapBuilder();
            logic = builder.GetState();
            view = new GameView();
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
            view.DrawScreen(logic.Player, logic.Enemies, logic.Obstacles, logic.Bombs);
		}
	}
}