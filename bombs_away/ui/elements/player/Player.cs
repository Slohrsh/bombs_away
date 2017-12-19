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
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize - (squareSize / 1.9f));
        }
        public Player(Box2D component)
        {
            this.Bounds = new Box2D(component);
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
            if (Keyboard.GetState()[Key.Down])
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
            Console.WriteLine(velocity);
            return base.Execute(updatePeriod);
        }
       
    }
}
