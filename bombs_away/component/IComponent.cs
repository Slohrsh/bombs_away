using bombs_away.ui;

namespace bombs_away.component
{
    public interface IComponent
    {
        void Initialize(GameObject gameObject);
        void Execute(float updatePeriod);
    }
}
