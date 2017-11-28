using bombs_away.ui.interactive;
using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;
using bombs_away.game;
using bombs_away.ui.enums;
using bombs_away.component.interactive;

namespace bombs_away.ui.elements.enemy
{
    class Enemy : MovableStaticMoves
    {
        public Enemy(Vector2 position, float squareSize)
        {
            this.body = Box2DFactory.CreateSquare(position, squareSize);
            ICollidable collidable = new Collidable();
            collidable.Initialize(this);
            components.Add(collidable);
        }
        public Enemy(Box2D component)
        {
            this.body = new Box2D(component);
            ICollidable collidable = new Collidable();
            collidable.Initialize(this);
            components.Add(collidable);
        }
    }
}
