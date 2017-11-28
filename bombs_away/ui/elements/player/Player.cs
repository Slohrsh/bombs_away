using bombs_away.component.interactive;
using bombs_away.component.physics;
using bombs_away.ui.interactive;
using bombs_away.ui.physics;
using bombs_away.ui.zenseless;
using OpenTK;
using OpenTK.Input;
using System;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.player
{
    class Player : GameObject
    {
        public event EventHandler onPlantBomb;

        private float timeDelta;
        public Player(Vector2 position, float squareSize)
        {
            this.body = Box2DFactory.CreateSquare(position, squareSize);
            ICollidable collidable = new Collidable();
            collidable.Initialize(this);
            components.Add(collidable);
            IRigidBody rigidBody = new RigidBody();
            rigidBody.Initialize(this);
            components.Add(rigidBody);
        }
        public Player(Box2D component)
        {
            this.body = new Box2D(component);
            ICollidable collidable = new Collidable();
            collidable.Initialize(this);
            components.Add(collidable);
            IRigidBody rigidBody = new RigidBody();
            rigidBody.Initialize(this);
            components.Add(rigidBody);
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
