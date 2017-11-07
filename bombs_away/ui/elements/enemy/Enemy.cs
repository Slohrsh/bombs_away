using bombs_away.ui.zenseless;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.elements.enemy
{
    class Enemy : Drawable
    {
        public Enemy(Vector2 position)
        {
            this.component = Box2DFactory.getSquare(position, 0.2f);
        }
    }
}
