using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.RenderUtils
{
    static class RenderHelper
    {
        private static GraphicsDevice _device;
        private static Texture2D _pixel;
        public static SpriteBatch SpriteBatch { get; private set; }

        public static void Init(GraphicsDevice device)
        {
            _device = device;
            _pixel = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            _pixel.SetData(new Color[] { Color.White });
            SpriteBatch = new SpriteBatch(device);
        }

        private static void Ensure()
        {
            if (_device == null)
                throw new InvalidOperationException("The RenderHelper was not initialized properly");
        }

        public static void Dot(Vector2 center, Color? color=null, float thickness=8f, SpriteBatch batch = null)
        {
            Ensure();
            if (batch == null) batch = SpriteBatch;
            if (color == null) color = Color.White;

            batch.Draw(_pixel, center, null, null, new Vector2(.5f, .5f), 0, Vector2.One * thickness, color, SpriteEffects.None, 0f);

        }

        public static void Line(Vector2 start, Vector2 end, 
            Color? color=null,
            float thickness=4,
            SpriteBatch batch=null)
        {
            Ensure();

            if (batch == null) batch = SpriteBatch;
            if (color == null) color = Color.White;

            var diff = (end - start);
            var middle = start + (diff / 2);
            var angle = (float) Math.Atan2(diff.Y, diff.X);

            batch.Draw(_pixel, middle, null, null, new Vector2(.5f, .5f), angle, new Vector2(diff.Length(), thickness), color, SpriteEffects.None, 0f);

        }


    }
}
