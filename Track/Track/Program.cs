﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Track
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new TrackGame())
                game.Run();
        }
    }
}
