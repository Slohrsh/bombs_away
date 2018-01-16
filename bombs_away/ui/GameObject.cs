using bombs_away.game;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bombs_away.ui.elements.player;
using Zenseless.Geometry;

namespace bombs_away.ui
{
    public class GameObject 
    {
        public event EventHandler<PositionUpdatedArgs> onPositionUpdate;    
        public Vector2 Position { get { return new Vector2(Bounds.MinX, Bounds.MinY); } }
        protected bool isVisible;
        private Vector2 oldCoordinates;

        public bool IsVisible { get { return isVisible; } }

        public Box2D Bounds { get; protected set; }
        public Box2D Hitbox
        {
            get
            {
                return new Box2D(
                    Bounds.MinX + Bounds.SizeX * 0.2f,
                    Bounds.MinY,
                    Bounds.SizeX * 0.6f,
                    Bounds.SizeY);
            }
        }

        public void setVisible()
        {
            isVisible = true;
        }

        public bool Intersects(GameObject rectangle)
        {
            if (rectangle == null)
            {
                return false;
            }
            return Bounds.Intersects(rectangle.Bounds);
        }

        public virtual Vector2 Execute(float updatePeriod)
        {
            int positionX = GridUtil.TransformPositionRelative(Bounds.CenterX, 0, ModelView.Instance.gridSize);
            int positionY = GridUtil.TransformPositionRelative(Bounds.CenterY, 0, ModelView.Instance.gridSize);
            if(oldCoordinates != Vector2.Zero)
            {
                if(oldCoordinates.X != positionX || oldCoordinates.Y != positionY)
                {
                    PositionUpdatedArgs args = new PositionUpdatedArgs();
                    args.OldCoordinates = oldCoordinates;
                    args.NewCoordinates = new Vector2(positionX, positionY);
                    oldCoordinates = args.NewCoordinates;
                    onPositionUpdate?.Invoke(this, args);
                }
            }
            else
            {
                oldCoordinates = new Vector2(positionX, positionY);
            }
            return Position;
        }
    }
}
