using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.MathUtils;

namespace Track.Wires
{
    static class WireGenerator
    {

        public static double DefaultSpotDensity { get; set; } = 50;

        public static Wire MakeRandomPath(Vector2 start, 
            float startAngle, float length, float targetDistance=200, float targetDistanceVariation=50, float targetAngleChange=.3f)
        {

            var spotCount = Math.Max(1, Math.Floor(length / DefaultSpotDensity));

            var spots = new List<WireSpot>();
            var parts = new List<WirePart>();

            var rand = new Random();
            var angle = startAngle;
            var curr = start;
            var prev = start - Vector2.UnitX;
            var distance = (float) (targetDistance + (rand.NextDouble() * targetDistanceVariation) - (targetDistanceVariation / 2));
            var target = curr + distance * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            for (var i = 0; i < spotCount; i++)
            {
                var vec = curr;
                spots.Add(new WireSpot(vec));

                // move towards target.
                var toTarget = target - curr;
                var toTargetUnit = toTarget.Normal();

                var advanceUnit = (toTargetUnit + (curr - prev).Normal()).Normal();
                curr += advanceUnit * (float)DefaultSpotDensity;
                if ( (curr - target).Length() < DefaultSpotDensity)
                {
                    distance = (float)(targetDistance + (rand.NextDouble() * targetDistanceVariation) - (targetDistanceVariation / 2));
                    angle += -(targetAngleChange / 2) + (float)(rand.NextDouble() * targetAngleChange);
                    target = curr + distance * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                }
            }
            for (var i = 0; i < spots.Count - 1; i++)
            {
                var ni = (i + 1) % spots.Count;
                parts.Add(new WirePart(spots[i], spots[ni]));
            }

            var wire = new Wire(spots.ToArray(), parts.ToArray());



            return wire;
        }

        public static Wire MakeCircle(Vector2 center, float radius)
        {
            
            var circ = MathHelper.TwoPi * radius;
            var spotCount = Math.Max(1, Math.Floor(circ / DefaultSpotDensity));

            var spots = new List<WireSpot>();
            var parts = new List<WirePart>();

            for (var i = 0; i < spotCount; i++)
            {
                var ratio = i / spotCount;
                var angle = MathHelper.TwoPi * ratio;
                var vec = radius * new Vector2( (float)Math.Cos(angle), (float)Math.Sin(angle));
                spots.Add(new WireSpot(center + vec));
            }

            for (var i = 0; i < spots.Count; i++)
            {
                var ni = (i + 1) % spots.Count;
                parts.Add(new WirePart(spots[i], spots[ni]));
            }

            var wire = new Wire(spots.ToArray(), parts.ToArray());

            return wire;
        }

    }
}
