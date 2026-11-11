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
        
        public List<Army> Armies { get; set; }
        
        public Vector2Int Frames { get; set; }
        
        public GameSnippet() { }

       
        public GameSnippet(List<Army> armies, Vector2Int frames)
        {
            Armies = armies;
            Frames = frames;
        }
    }
}
