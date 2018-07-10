using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.RenderUtils;

namespace Track.Wires
{
    class WireSpotRenderOverride
    {
        public Color DotColor { get; set; }
    }

    class WireRenderer
    {
        
        public Dictionary<WireSpot, WireSpotRenderOverride> SpotOverrides { get; private set; }

        public WireRenderer()
        {
            SpotOverrides = new Dictionary<WireSpot, WireSpotRenderOverride>();
        }

        public void ClearSpotOverrides()
        {
            SpotOverrides.Clear();
        }

        public void Render(RenderArgs args, Wire wire)
        {
            foreach (WirePart part in wire.Parts)
            {
                RenderHelper.Line(part.A.Position, part.B.Position);
            }
            foreach (WireSpot spot in wire.Spots)
            {
                var custom = new WireSpotRenderOverride();
                if (SpotOverrides.ContainsKey(spot))
                {
                    custom = SpotOverrides[spot];
                }
                RenderHelper.Dot(spot.Position, custom.DotColor, 4f);
            }

        }

    }
}
