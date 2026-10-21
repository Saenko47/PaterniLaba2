using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class CombatContext
    {
        public Vector2Int framesOfTable { get; set; }

        public List<BaseUnit> opponentTeam { get; set; } = null!;

        public Action<BaseUnit> onUnitDeath { get; set; } = null!;

        public Func<int, int, bool> isCellFree { get; set; } = null!;

        public Action<Vector2Int, Vector2Int> onMove { get; set; } = null!;

    }
}
