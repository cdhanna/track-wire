using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.Wires;

namespace Track.Players
{
    class WireRider
    {
        public Wire Wire { get; set; }

        private int _partIndex = 0;
        private float _currentRatio = 0;

        public WireRider()
        {

        }

        public WirePart GetCurrentPart()
        {
            return Wire.Parts[_partIndex];
        }

        public void Move(float distance)
        {

            var distanceLeft = Math.Abs(distance);
            var direction = Math.Sign(distance);
            var part = Wire.Parts[_partIndex];
            var ratioInc = distance / part.Length; // 8 / 10 = 80%. 11 / 10 = 110%;

            while (_currentRatio + ratioInc >= 1)
            {
                var old = _partIndex;
                _partIndex = Wire.NextPartIndex(_partIndex);
                if (old == _partIndex)
                {
                    ratioInc = 0;
                    _currentRatio = 1;
                    break;
                }
                distance -= part.Length;
                part = Wire.Parts[_partIndex];
                ratioInc = distance / part.Length;
            }
            while (_currentRatio + ratioInc <= 0)
            {
                var old = _partIndex;
                _partIndex = Wire.PreviousPartIndex(_partIndex);
                if (old == _partIndex)
                {
                    ratioInc = 0;
                    _currentRatio = 0;
                    break;
                }
                distance += part.Length;
                part = Wire.Parts[_partIndex];
                ratioInc = distance / part.Length;
            }
            _currentRatio += ratioInc;


            
        }

        public Vector2 GetPosition()
        {

            var part = Wire.Parts[_partIndex];
            var start = part.A.Position;
            var end = part.B.Position;
            var all = end - start;

            return start + (_currentRatio * all);

        }
    }
}
