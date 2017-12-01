using bombs_away.game;
using bombs_away.ui;
using System;

namespace bombs_away.component.collision
{
    class Collider : ICollider
    {
        public event EventHandler onCollision;

        private GameObject gameObject;


        public void Initialize(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public void Execute(float updatePeriod)
        {
            ResolveCollision();
        }

        public void ResolveCollision()
        {
            ModelView model = ModelView.Instance;
            UndoOverlapRelativeToComponent(-1, -1, model);
            UndoOverlapRelativeToComponent(0, -1, model);
            UndoOverlapRelativeToComponent(1, -1, model);

            UndoOverlapRelativeToComponent(-1, 1, model);
            UndoOverlapRelativeToComponent(0, 1, model);
            UndoOverlapRelativeToComponent(1, 1, model);

            UndoOverlapRelativeToComponent(-1, 0, model);
            UndoOverlapRelativeToComponent(0, 0, model);
            UndoOverlapRelativeToComponent(1, 0, model);

        }

        private void UndoOverlapRelativeToComponent(int x, int y, ModelView model)
        {
            int positionX = TransformPositionRelative(gameObject.Body.CenterX, x);
            int positionY = TransformPositionRelative(gameObject.Body.CenterY, y);

            if (positionX >= 0 && positionX <= (int)StaticValues.GRIDSIZE-1 &&
                positionY >= 0 && positionY <= (int)StaticValues.GRIDSIZE-1)
                {
                Block block = model.StaticGrid[positionX, positionY];
                onCollision?.Invoke(block, null);
            }
        }

        private int TransformPositionRelative(float componentPosition, int position)
        {
            int relativePosition = (int)(componentPosition * 10);
            return relativePosition + position;
        }
    }
}
