using bombs_away.ui;
using bombs_away.ui.elements.ground;
using System.Collections.Generic;
using System;
using System.Numerics;

namespace bombs_away.game
{
    public class ModelView
    {
        public List<Block>[,] Grid { get; set; }
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

        public void AddComponentToGrid(Block block)
        {
            Vector2 actualCoordiantes = CalculateActualCoordiantes(block);
            Grid[(int)actualCoordiantes.X, (int)actualCoordiantes.Y].Add(block);
        }

        public void UpdateGrid(object sender, PositionUpdatedArgs args)
        {
            GameObject gameObject = (GameObject)sender;
            Block block = null;
            foreach(Block potentionalBlock in Grid[(int)args.OldCoordinates.X, (int)args.OldCoordinates.Y])
            {
                if(potentionalBlock.Bounds.Equals(gameObject.Bounds))
                {
                    block = potentionalBlock;
                }
            }
            if(block != null)
            {
                Grid[(int)args.OldCoordinates.X, (int)args.OldCoordinates.Y].Remove(block);
                Grid[(int)args.NewCoordinates.X, (int)args.NewCoordinates.Y].Add(block);
            }
        }

        public void RegisterComponent(GameObject origin, Block reference)
        {
            reference.Bounds = origin.Bounds;
            origin.onPositionUpdate += (sender, args) => UpdateGrid(sender, args);
        }

        private Vector2 CalculateActualCoordiantes(Block block)
        {
            int positionX = GridUtil.TransformPositionRelative(block.Bounds.CenterX, 0, gridSize);
            int positionY = GridUtil.TransformPositionRelative(block.Bounds.CenterY, 0, gridSize);
            return new Vector2(positionX, positionY);
        }
    }
}