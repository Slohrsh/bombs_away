using bombs_away.ui;
using System.Collections.Generic;

namespace bombs_away.game
{
    public class ModelView
    {
        public GameObject Player { get; set; }
        public List<GameObject> Enemies { get; set; }
        public List<GameObject> Obstacles { get; set; }
        public List<GameObject> Bombs { get; set; }
        public GameObject Portal { get; set; }

    }
}