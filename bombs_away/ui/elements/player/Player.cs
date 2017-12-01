using bombs_away.component.physics;
using bombs_away.component.collision;
using bombs_away.game;
using bombs_away.ui.enums;
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
        private IRigidBody rigidBody;
        private bool grounded;

        public bool Grounded { set { grounded = value; } }


        private float timeDelta;
        public Player(Vector2 position, float squareSize)
        {
            this.body = Box2DFactory.CreateSquare(position, squareSize);
            RegisterComponents();
        }
        public Player(Box2D component)
        {
            this.body = new Box2D(component);
            RegisterComponents();
        }

        private void RegisterComponents()
        {
            ICollider collider = new Collider();
            collider.Initialize(this);
            collider.onCollision += (sender, args) => UndoOverlap(sender, args);
            components.Add(collider);
            rigidBody = new RigidBody();
            rigidBody.Initialize(this, 1f);
            components.Add(rigidBody);
        }
        private void HandleUserInput(float updatePeriod)
        {

            timeDelta += updatePeriod;

            if (Keyboard.GetState()[Key.Left] || Keyboard.GetState()[Key.A])
            {
                rigidBody.AddForce(new Vector2(-1 * updatePeriod, 0));
                //MoveX(-1 * updatePeriod);
                //ShiftLeft(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Right] || Keyboard.GetState()[Key.D])
            {
                rigidBody.AddForce(new Vector2(updatePeriod, 0));
                //MoveX(updatePeriod);
            }
            if (Keyboard.GetState()[Key.Space])
            {
                Console.WriteLine("Jump");
                if(grounded)
                {
                    rigidBody.AddForce(new Vector2(0, updatePeriod * 0.10f));
                }
                //Jump(updatePeriod);
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

        private void UndoOverlap(object sender, EventArgs args)
        {
            Block block = (Block)sender;
            if (block.Type == BlockType.GROUND)
            {
                Box2D ground = block.Component;
                if (body.Intersects(ground))
                {
                    Directions pushDirection =
                        Box2DextensionsCustom.UndoOverlap(body, ground);
                    if(pushDirection == Directions.UP)
                    {
                        grounded = true;
                    }
                }
            }
        }

    }
}
