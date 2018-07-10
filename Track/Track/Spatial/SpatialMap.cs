using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.Spatial
{
    class SpatialMap<T>
    {
        public int CellSize { get; set; }

        private Dictionary<int, List<T>> _map;

        public SpatialMap(int cellSize=100)
        {
            CellSize = cellSize;
            _map = new Dictionary<int, List<T>>();
        }

        public void Clean()
        {
            _map.Clear();
        }

        public void Register(Vector2 position, T content)
        {
            var hash = HashPosition(position);

            // get cell
            if (!_map.ContainsKey(hash))
                _map.Add(hash, new List<T>());

            _map[hash].Add(content);
        }

        public IList<T> GetNeighbors(Vector2 position, int ring=0)
        {
            var entireSet = new List<T>();
            for (var x = -ring; x <= ring; x++)
            {
                for (var y = -ring; y <= ring; y++)
                {
                    var adj = new Vector2(x * CellSize, y * CellSize);

                    var hash = HashPosition(position + adj);
                    var set = new List<T>();
                    if (_map.ContainsKey(hash))
                        set = _map[hash];
                    entireSet.AddRange(set);
                }
            }
            

            return entireSet;
        }

        private Vector2 SnapPosition(Vector2 position)
        {
            /* Ex:  grid size = 100.
             *      x = 430
             *      output should be 400
             *      
             *      x / 100 = 4. Math.Floor(x / gridSize) * gridSize;
             */
            return new Vector2(
                (float) Math.Floor(position.X / CellSize) * CellSize,
                (float) Math.Floor(position.Y / CellSize) * CellSize);
        }

        private int HashPosition(Vector2 position)
        {
            var snapped = SnapPosition(position);
            var str = snapped.X + ":" + snapped.Y;
            return str.GetHashCode();
        }

    }
}
