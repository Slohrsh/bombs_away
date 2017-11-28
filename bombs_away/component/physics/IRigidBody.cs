namespace bombs_away.component.physics
{
    internal interface IRigidBody : IComponent
    {
        void AddForceX(float force);
        void AddForceY(float force);
    }
}