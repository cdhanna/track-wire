using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track.InputUtils
{
    static class KeyboardHelper
    {

        private static KeyboardState _curr, _prev;

        public static void Update()
        {
            _curr = Keyboard.GetState();
            _prev = _curr;
        }

        public static bool IsDown(Keys k)
        {
            return _curr.IsKeyDown(k);
        }
        public static bool IsNewDown(Keys k)
        {
            return _curr.IsKeyDown(k) && !_prev.IsKeyDown(k);
        }

    }
}
