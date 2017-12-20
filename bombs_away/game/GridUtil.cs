using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bombs_away.game
{
    public static class GridUtil
    {
        public static int TransformPositionRelative(float componentPosition, int position, int gridSize)
        {
            int relativePosition = (int)(componentPosition * gridSize);
            return relativePosition + position;
        }
    }
}
