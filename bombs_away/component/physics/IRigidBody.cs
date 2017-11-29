using bombs_away.ui;
using OpenTK;

namespace bombs_away.component.physics
{
    internal interface IRigidBody : IComponent
    {
        void AddForce(Vector2 direction);
        void Initialize(GameObject gameObject, float mass);
    }
}