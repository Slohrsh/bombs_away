using Zenseless.Application;
using Zenseless.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using bombs_away.ui.elements.player;
using bombs_away.controller;

namespace bombs_away.game
{
	class Controller
	{

        private GameLogic logic;
        private GameView view;
        private GameMapBuilder builder;

        public Controller()
        {
            builder = new GameMapBuilder();
            logic = builder.GetState();
            view = new GameView();
    }
		public void Update(float updatePeriod)
		{
            float axisLeftRight = Keyboard.GetState()[Key.Left] ? -1.0f : Keyboard.GetState()[Key.Right] ? 1.0f : 0.0f;
            logic.Update(updatePeriod, axisLeftRight);
		}

		public void Render()
		{
            view.DrawScreen(logic.Player, logic.Enemies, logic.Obstacles, logic.Bombs);
		}
	}
}