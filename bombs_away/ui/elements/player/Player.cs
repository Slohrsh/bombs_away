using bombs_away.ui.interactive;
using bombs_away.ui.physics;
using bombs_away.ui.zenseless;
using OpenTK;
using OpenTK.Input;
using System;

namespace bombs_away.ui.elements.player
{
    class Player : PhysicsObject
    {
        public event EventHandler onPlantBomb;

        private float timeDelta;
        public Player(Vector2 position, float squareSize)
        {
            this.component = Box2DFactory.CreateSquare(position, squareSize);
        }
        private void HandleUserInput(float updatePeriod)
        {

            timeDelta += updatePeriod;

            if (Keyboard.GetState()[Key.Left] || Keyboard.GetState()[Key.A])
            {
                MoveX(-1 * updatePeriod);
                //ShiftLeft(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Right] || Keyboard.GetState()[Key.D])
            {
                MoveX(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Space])
            {
                Console.WriteLine("Jump");
                Jump(updatePeriod);
            }
            if (Keyboard.GetState()[Key.E])
            {
                if (timeDelta > 3f)
                {
                    plantBomb();
                    timeDelta = 0;
                }
            }
        }

        public void plantBomb()
        {
            onPlantBomb?.Invoke(this, null);
        }

        public override void Execute(float updatePeriod)
        {
            HandleUserInput(updatePeriod);
            base.Execute(updatePeriod);
        }
       
    }
}
