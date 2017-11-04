using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.ui.elements.enemy
{
    class Enemy : Drawable
    {
        public Enemy()
        {
            this.component = new Zenseless.Geometry.Box2D(0.0f, -0.95f, 0.2f, 0.2f);
        }
    }
}
