using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.MathUtils;

namespace Track.RenderUtils
{
    class Camera
    {

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; private set; }
        public float Friction { get; private set; } = .1f;
        public Vector2 SigmaForce { get; private set; } = Vector2.Zero;

        public float Zoom { get; set; } = 1;
        public float ZoomVelocity { get; set; }
        public float ZoomFriction { get; set; } = .9f;
        public float ZoomSigmaForce { get; set; }

        public Matrix Transform { get; private set; }

        public Camera()
        {
            Position = Vector2.Zero;
        }

        public void ApplyForce(Vector2 force)
        {
            SigmaForce += force;
        }

        public void ApplyZoomForce(float zoom)
        {
            ZoomSigmaForce += zoom;
        }

        public void Update(GameTime gameTime)
        {
            var mat = Matrix.Identity;

            var acc = SigmaForce / Zoom;
            acc += -Velocity * Friction;
            Velocity += acc;
            Position += Velocity;
            SigmaForce = Vector2.Zero;

            var zoomAcc = ZoomSigmaForce;
            zoomAcc += -ZoomVelocity * ZoomFriction;
            ZoomVelocity += zoomAcc;
            Zoom += ZoomVelocity;
            Zoom = Math.Max(Zoom, .3f);
            Zoom = Math.Min(Zoom, 1.5f);
            ZoomSigmaForce = 0;

            mat *= Matrix.CreateTranslation(-Position.ToVec3());
            mat *= Matrix.CreateScale(Zoom);

            Transform = mat;
        }

    }
}
