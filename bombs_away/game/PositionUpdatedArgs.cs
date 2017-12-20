using OpenTK;
using System;

namespace bombs_away.game
{
    public class PositionUpdatedArgs : EventArgs
    {
        public Vector2 OldCoordinates { get; set; }
        public Vector2 NewCoordinates { get; set; }
    }
}