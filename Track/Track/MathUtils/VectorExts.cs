using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.MathUtils
{
    static class VectorExts
    {
        public static Vector2 Normal(this Vector2 v)
        {
            if (Math.Abs(v.Length()) < .0001f) return Vector2.Zero;

            return v / v.Length();
        }

        public static Vector3 Normal(this Vector3 v)
        {
            var l = v.Length();
            if (Math.Abs(l) < .0001f) return Vector3.Zero;
            return v / l;
        }

        public static Vector2 Perp(this Vector2 v)
        {
            return new Vector2(v.Y, -v.X);
        }

        //public static Vector3 Perp(this Vector3 v, Vector3 up)
        //{
        //    Vector3.Cross()
        //}
        
        public static Vector3 ToVec3(this Vector2 v, float z = 0f)
        {
            return new Vector3(v.X, v.Y, z);
        }
    }
}
