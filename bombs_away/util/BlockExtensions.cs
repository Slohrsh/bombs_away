using bombs_away.game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenseless.Geometry;

namespace bombs_away.util
{
    static class BlockExtensions
    {
        public static void RemoveReferencedObject(this Box2D component, List<Block> list)
        {
            foreach (Block block in list.ToList())
            {
                if (component.Equals(block.Component))
                {
                    list.Remove(block);
                }
            }
        }

        public static void SetReferencedBlockVisible(this Box2D component, List<Block> list)
        {
            foreach (Block block in list.ToList())
            {
                if (component.Equals(block.Component))
                {
                    block.IsVisible = true;
                }
            }
        }
    }
}
