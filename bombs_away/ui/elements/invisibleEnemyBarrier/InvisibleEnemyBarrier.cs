using bombs_away.ui.zenseless;
using OpenTK;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.InvisibleEnemyBarrier
{
    public class InvisibleEnemyBarrier : GameObject
    {
        public InvisibleEnemyBarrier(Vector2 position, float squareSize)
        {
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize);
        }
        public InvisibleEnemyBarrier(Box2D component)
        {
            this.Bounds = new Box2D(component);
        }
    }
}