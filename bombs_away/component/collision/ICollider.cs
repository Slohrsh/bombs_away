using System;

namespace bombs_away.component.collision
{
    internal interface ICollider : IComponent
    {
        event EventHandler onCollision;
        void ResolveCollision();
    }
}