using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.RenderUtils
{
    class RenderArgs
    {
        public Vector2 Viewport { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public RenderArgs(SpriteBatch batch, Vector2 viewport)
        {
            SpriteBatch = batch;
            Viewport = viewport;
        }
    }
}
