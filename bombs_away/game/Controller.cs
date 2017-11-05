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

namespace bombs_away.game
{
	class Controller
	{

        private GameLogic logic;
        private GameView view;
        private GameMapBuilder builder;

        private Stopwatch timeSource = new Stopwatch();
        private float lastUpdateTime = 0.0f;

        public Controller()
        {
            builder = new GameMapBuilder();
            logic = builder.GetState();
            view = new GameView();
            logic.onLost += (sender, args) => { };
            logic.onThrowBomb += (sender, args) => {  };
            logic.onEnemyDestroy += (sender, args) => { };
            timeSource.Start();
        }
		public void Update(float updatePeriod)
		{
            float absoluteTime = (float)timeSource.Elapsed.TotalSeconds;
            var timeDelta = absoluteTime - lastUpdateTime;
            lastUpdateTime = absoluteTime;

            Movement movement = getUserInput();
            logic.Update((float)timeSource.Elapsed.TotalSeconds, updatePeriod, movement);
		}

        private Movement getUserInput()
        {
            if (Keyboard.GetState()[Key.Left])
            {
                return Movement.LEFT;
            }
            if (Keyboard.GetState()[Key.Right])
            {
                return Movement.RIGTH;
            }
            if (Keyboard.GetState()[Key.Space])
            {
                return Movement.JUMP;
            }
            return Movement.IDLE;
        }

        public void Render()
		{
            view.DrawScreen(logic.Player, logic.Enemies, logic.Obstacles, logic.Bombs);
		}
	}
}