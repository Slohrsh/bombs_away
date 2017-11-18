using bombs_away.ui.interactive;
using bombs_away.ui.zenseless;
using OpenTK;

namespace bombs_away.ui.elements.player
{
    class Player : MovableUserInput
    {
        public Player(Vector2 position, float squareSize)
        {
            this.component = Box2DFactory.CreateSquare(position, squareSize);
        }
    }
}
