using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.RenderUtils;

namespace Track.DebugUtil
{
    class GridOverlay
    {
        public Vector2 Size { get; set; }
        public GridOverlay(Vector2 size)
        {
            Size = size;
        }
        public GridOverlay() : this(new Vector2(100, 100)) { }



        public void Render(RenderArgs args)
        {
            for (var x = 0f; x < args.Viewport.X; x += Size.X)
            {
                var y0 = 0;
                var y1 = args.Viewport.Y;
                RenderHelper.Line(new Vector2(x, y0), new Vector2(x, y1), Color.Gray, 1);   
            }
            for (var y = 0f; y < args.Viewport.Y; y += Size.Y)
            {
                var x0 = 0;
                var x1 = args.Viewport.X;
                RenderHelper.Line(new Vector2(x0, y), new Vector2(x1, y), Color.Gray, 1);
            }
        }
    }
}
