using bombs_away.ui.zenseless;
using OpenTK;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.portal
{
    class Portal : GameObject
    {
        public Portal(Vector2 position, float squareSize)
        {
            isVisible = false;
            this.Bounds = Box2DFactory.CreateSquare(position, squareSize);
        }
        public Portal(Box2D component)
        {
            this.Bounds = new Box2D(component);
        }
    }
}
