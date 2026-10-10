using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal class Cell
    {
        public BaseUnit? placeForUnit { get; set; }
        public int x { get; private set; }
        public int y { get; private set; }

        public Cell(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
    }
}
