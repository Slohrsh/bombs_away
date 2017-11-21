using bombs_away.ui.zenseless;
using OpenTK;


namespace bombs_away.ui.elements.portal
{
    class Portal : GameObject
    {
        public Portal(Vector2 position, float squareSize)
        {
            isVisible = false;
            this.component = Box2DFactory.CreateSquare(position, squareSize);
        }

        public void setVisible()
        {
            isVisible = true;
        }
    }
}
