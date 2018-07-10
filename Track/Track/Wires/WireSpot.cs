using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.Wires
{
    class WireSpot
    {
        private static int idC = 0;
        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 RestPosition { get; private set; }
        public float PositionCoef { get; private set; } = .001f;

        public float FrictionCoef { get; private set; } = .02f;
        public Vector2 Velocity { get; set; }
        public Vector2 SigmaForce { get; private set; }
        public int Mass { get; private set; } = 1;

        public WireSpot(Vector2 pos)
        {
            Id = idC++;
            Position = pos;
            RestPosition = pos;
            SigmaForce = Vector2.Zero;
        }

        public void ApplyForce(Vector2 force)
        {
            SigmaForce += force;
        }

        public void Update(GameTime time)
        {
            var dt = time.ElapsedGameTime.Milliseconds;

            var acc = SigmaForce / Mass;// f = ma; a = f/m
            acc += -Velocity * FrictionCoef;
            acc += (RestPosition - Position) * PositionCoef;
            Velocity += acc;// * (dt / 2);

            Position += Velocity;// * dt;

            SigmaForce = Vector2.Zero;
        }

        public override int GetHashCode()
        {
            return Id;
        }
        public override bool Equals(object obj)
        {
            var other = obj as WireSpot;
            if (other == null) return false;

            return other.Id.Equals(Id);
        }
    }
}
