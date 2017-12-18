using bombs_away.ui;
using bombs_away.ui.elements.ground;
using System.Collections.Generic;

namespace bombs_away.game
{
    public class ModelView
    {
        public Block[,] ConstantGrid { get; set; }
        public List<Block> InteractiveObjects { get; set; }
        public int gridSize { get; set; }

        private static ModelView instance;

        private ModelView() { }

        public static ModelView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModelView();
                }
                return instance;
            }
        }

    }
}