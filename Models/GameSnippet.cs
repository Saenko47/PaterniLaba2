using PaterniLab2.Models.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PaterniLab2.Models
{
    internal class GameSnippet
    {
        public List<Army> _armies { get; private set; }
        public Vector2Int _frames { get; private set; }

        public GameSnippet(List<Army> armies, Vector2Int frames) 
        {
            _armies = armies;
            _frames = frames;
        }
    }
}
