using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests
{
    internal class RequestOnHighlihtUnit
    {
       public Vector2Int position { get; set; }
        public HighlightType highlightType { get; set; }
    }
}
