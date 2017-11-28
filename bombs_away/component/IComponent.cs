using bombs_away.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.component
{
    public interface IComponent
    {
        void Initialize(GameObject gameObject);
        void Execute(float updatePeriod);
    }
}
