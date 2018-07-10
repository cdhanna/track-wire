using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.MathUtils;

namespace Track.Wires
{
    class WirePart
    {
        public WireSpot A { get; private set; }
        public WireSpot B { get; private set; }

        public float Length
        {
            get
            {
                return (B.Position - A.Position).Length();
            }
        }

        public Vector2 Perpendicular
        {
            get
            {
                return (B.Position - A.Position).Normal().Perp();
            }
        }

        public float DirectionCoef { get; private set; } = .1f;

        public WirePart(WireSpot a, WireSpot b)
        {
            A = a;
            B = b;
        }
        
        public void Update(GameTime gameTime)
        {
            var currDiff = A.Position - B.Position;
            var restDiff = A.RestPosition - B.RestPosition;
            var diff = restDiff - currDiff;

            //var result = -currDiff.Normal() * 1f * DirectionCoef * (float) Math.Pow(Math.Abs(diff.Length()),.1);//
            var result = diff.Normal() * DirectionCoef * (float)Math.Pow(Math.Abs(diff.Length()), 1.3);//


            if (float.IsNaN(result.X))
            {

            }
            A.ApplyForce(result / 2);
            B.ApplyForce(-result / 2);
        }

    }
}
