using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    public readonly struct Vector2Int
    {
        public int X { get; }
        public int Y { get; }

        public Vector2Int(int x, int y) => (X, Y) = (x, y);
    }
}
