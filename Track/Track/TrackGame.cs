using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.DebugUtil;
using Track.InputUtils;
using Track.MathUtils;
using Track.Players;
using Track.RenderUtils;
using Track.Spatial;
using Track.Wires;

namespace Track
{
    public class TrackGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private Wire _wire;
        private WireRenderer _wireRenderer;
        private GridOverlay _grid;
        private Vector2 _pos, _vel;
        private Player _player;
        private Camera _camera;

        private SpatialMap<WireSpot> _spotMap;

        public TrackGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            RenderHelper.Init(GraphicsDevice);
            _camera = new Camera();
            _grid = new GridOverlay();
            _spotMap = new SpatialMap<WireSpot>(80);
            _grid.Size = new Vector2(_spotMap.CellSize);

            _wireRenderer = new WireRenderer();
            WireGenerator.DefaultSpotDensity = 10;
            //_wire = WireGenerator.MakeCircle(new Vector2(350, 250), 200);
            _wire = WireGenerator.MakeRandomPath(new Vector2(50, 200), 0, 500, 50, 0, .5f);

            _player = new Player(_wire);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var accl = 1.5f;
            var fric = .2f;
            var acc = new Vector2();
            if (KeyboardHelper.IsDown(Keys.Left))
                acc -= Vector2.UnitX * accl;
            if (KeyboardHelper.IsDown(Keys.Right))
                acc += Vector2.UnitX * accl;
            if (KeyboardHelper.IsDown(Keys.Down))
                acc += Vector2.UnitY * accl;
            if (KeyboardHelper.IsDown(Keys.Up))
                acc -= Vector2.UnitY * accl;
            _vel += acc;
            _vel += -_vel * fric;
            _pos += _vel;

            _spotMap.Clean();
            foreach (WireSpot spot in _wire.Spots)
            {
                _spotMap.Register(spot.Position, spot);
            }
            _wireRenderer.ClearSpotOverrides();
            var neighbors = _spotMap.GetNeighbors(_pos, 1);
            foreach (WireSpot spot in neighbors)
            {

                _wireRenderer.SpotOverrides.Add(spot, new WireSpotRenderOverride()
                {
                    DotColor = Color.Green
                });
                var dist = (spot.Position - _pos).Length();
                if (dist < 80)
                {
                    _wireRenderer.SpotOverrides[spot].DotColor = Color.Red;
                    var force = (spot.Position - _pos).Normal() * -.3f ;
                    spot.ApplyForce(force);
                }
            }

            _wire.Update(gameTime);
            _player.Update(gameTime);


            var camForce = Vector2.Zero;
            var camZoomForce = 0f;
            if (KeyboardHelper.IsDown(Keys.I))
                camForce += -Vector2.UnitY;
            if (KeyboardHelper.IsDown(Keys.K))
                camForce += Vector2.UnitY;
            if (KeyboardHelper.IsDown(Keys.J))
                camForce += -Vector2.UnitX;
            if (KeyboardHelper.IsDown(Keys.L))
                camForce += Vector2.UnitX;
            if (KeyboardHelper.IsDown(Keys.U))
                camZoomForce -= .1f;
            if (KeyboardHelper.IsDown(Keys.O))
                camZoomForce += .1f;
            _camera.ApplyForce(camForce * .8f);
            _camera.ApplyZoomForce(camZoomForce * .1f);
            _camera.Update(gameTime);
            KeyboardHelper.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var viewport = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            var args = new RenderArgs(RenderHelper.SpriteBatch, viewport);
            RenderHelper.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.DepthRead, RasterizerState.CullCounterClockwise, null, _camera.Transform);

            _grid.Render(args);
            RenderHelper.Dot(_pos, Color.Green);
            _wireRenderer.Render(args, _wire);

            _player.Render(args);

            RenderHelper.SpriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
