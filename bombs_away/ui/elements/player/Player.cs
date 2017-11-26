using bombs_away.ui.interactive;
using bombs_away.ui.physics;
using bombs_away.ui.zenseless;
using OpenTK;
using OpenTK.Input;
using System;
using Zenseless.Geometry;

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
        public Player(Box2D component)
        {
            this.component = new Box2D(component);
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

        public override Vector2 Execute(float updatePeriod)
        {
            HandleUserInput(updatePeriod);
            return base.Execute(updatePeriod);
        }
       
    }
}
