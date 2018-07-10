using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.Wires
{
    class Wire
    {
        public WireSpot[] Spots { get; private set; }
        public WirePart[] Parts { get; private set; }

        public Wire(WireSpot[] spots, WirePart[] parts)
        {
            // copy the arrays to avoid contamination later on.
            Spots = new WireSpot[spots.Length];
            Array.Copy(spots, Spots, spots.Length);
            Parts = new WirePart[parts.Length];
            Array.Copy(parts, Parts, parts.Length);

        }

        public int NextPartIndex(int index)
        {
            var part = Parts[index];
            // normally, we just increment
            if (index < Parts.Length - 1)
            {
                return index + 1;
            }

            // but if we are at the end, either it is a dead end, or it loops
            if (Parts[0].A.Equals(Parts[Parts.Length - 1].B))
            {
                return 0;
            } else
            {
                return index;
            }
        }
        public int PreviousPartIndex(int index)
        {
            var part = Parts[index];
            // normally, we just decrement
            if (index > 0)
            {
                return index - 1;
            }

            // but if we are at the end, either it is a dead end, or it loops
            if (Parts[0].A.Equals(Parts[Parts.Length - 1].B))
            {
                return Parts.Length - 1;
            }
            else
            {
                return index;
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (WirePart part in Parts)
            {
                part.Update(gameTime);
            }
            foreach (WireSpot spot in Spots)
            {
                spot.Update(gameTime);
            }
        }

    }
}
