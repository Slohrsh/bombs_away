using OpenTK.Graphics;
using Zenseless.Geometry;

namespace bombs_away.ui.elements.obstacle
{
    internal interface IDrawable
    {
        Box2D Component { get; }
        Color4 Color { get; }
    }
}