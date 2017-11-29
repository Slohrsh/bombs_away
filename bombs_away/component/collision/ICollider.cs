
using System;

namespace bombs_away.component.interactive
{
    internal interface ICollider : IComponent
    {
        event EventHandler onCollision;
        void ResolveCollision();
    }
}