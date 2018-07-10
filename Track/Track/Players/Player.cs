using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.InputUtils;
using Track.RenderUtils;
using Track.Wires;

namespace Track.Players
{
    class Player
    {
        private WireRider _rider;
        private Wire _wire;

        private float _speed = .6f;
        private float _friction = .1f;
        private float _wireVelocity = 0;

        public Player(Wire wire)
        {
            _wire = wire;
            _rider = new WireRider();
            _rider.Wire = wire;
        }

        public Vector2 GetPosition()
        {
            return _rider.GetPosition();
        }

        public void Update(GameTime gameTime)
        {
            // handle input
            var acc = 0f;
            if (KeyboardHelper.IsDown(Keys.Q))
            {
                acc += 1 * _speed;
            }
            if (KeyboardHelper.IsDown(Keys.W))
            {
                acc -= 1 * _speed;
            }

            _wireVelocity += acc;
            _wireVelocity += -_wireVelocity * _friction;

            var currPart = _rider.GetCurrentPart();
            var force = .06f * Math.Abs(_wireVelocity) * currPart.Perpendicular;
            _rider.Move(_wireVelocity);
            _rider.GetCurrentPart().A.ApplyForce(force);
            _rider.GetCurrentPart().B.ApplyForce(force);




        }

        public void Render(RenderArgs args)
        {
            RenderHelper.Dot(GetPosition(), Color.Red, 12);
        }
    }
}
