﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    /// <summary>
    /// This class is used to set up the interface and the rows and columns
    /// </summary>
    public class GameStep
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Player Player { get; set; }
    }
}
