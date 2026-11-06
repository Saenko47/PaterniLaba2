using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class TableContext
    {
        public Vector2Int framesOfTable { get; set; }

        public List<Army> armies { get; set; } = null!;


        public Func<int, int, bool> isCellFree { get; set; } = null!;

        public Action<Vector2Int, Vector2Int> onMove { get; set; } = null!;

       
       

    }
}
