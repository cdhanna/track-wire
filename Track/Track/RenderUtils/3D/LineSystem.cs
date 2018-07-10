using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Track.MathUtils;

namespace Track.RenderUtils._3D
{
    class LineSystem
    {
        private const int VB_START_SIZE = 1024;
        private const int IB_START_SIZE = 1024;
        private struct LineData
        {
            public VertexPositionColor[] Data;
        };

        private VertexBuffer _vb;
        private IndexBuffer _ib;
        private GraphicsDevice _device;

        private List<LineData> _lines;


        public LineSystem(GraphicsDevice device)
        {
            _device = device;
            CreateBuffers(VB_START_SIZE, IB_START_SIZE);
        }

        public void Begin()
        {

            _device.SetVertexBuffer(_vb);
            _device.Indices = _ib;
        }

        public void End()
        {

        }

        public void Line(List<Vector3> points, Color color, float thickness=1, int resolution=3)
        {
            if (points.Count < 2)
                throw new ArgumentException("Length of points must be at least 2");
            if (resolution < 3)
                throw new ArgumentException("Resolution must be at least 3");
            if (thickness == 0)
                throw new ArgumentException("thickness must be greater than 0");

            var output = new VertexPositionColor[points.Count * resolution];
            var indicies = new int[ (points.Count - 1) * resolution * 6];

            for (var i = 0; i < points.Count - 1; i++)
            {
                var a = points[i];
                var b = points[i + 1];

                var toB = (b - a).Normal();

                var w = Vector3.Zero;
                if (toB.Z < toB.X && toB.Z < toB.Y)
                {
                    w = Vector3.UnitZ;
                } else if (toB.X < toB.Z && toB.X < toB.Y)
                {
                    w = Vector3.UnitX;
                } else w = Vector3.UnitY;
                var u = Vector3.Cross(w, toB);
                var v = Vector3.Cross(toB, u);

                for (var j = 0; j < resolution; j++)
                {
                    // plane defined by A and toB
                    var angle = MathHelper.TwoPi / (j + 1);
                    var cos = (float)Math.Cos(angle);
                    var sin = (float)Math.Sin(angle);

                    var pos = a 
                        + (u * cos * thickness) 
                        + (v * sin * thickness);

                    output[i + j] = new VertexPositionColor(pos, color);
                    if (i == points.Count - 2)
                    {
                        var posB = pos + b - a;
                        output[i + j + resolution] = new VertexPositionColor(posB, color);
                    }
                }

                //output[i] = new VertexPositionColor()
            }

            _lines.Add(new LineData()
            {
                Data = output
            });
        }

        private void Flush()
        {
            
            
        }

        private void CreateBuffers(int vbSize, int ibSize)
        {
            _vb = new VertexBuffer(_device, typeof(VertexPositionColor), vbSize, BufferUsage.WriteOnly);
            _ib = new IndexBuffer(_device, IndexElementSize.ThirtyTwoBits, ibSize, BufferUsage.WriteOnly);
        }

    }
}
