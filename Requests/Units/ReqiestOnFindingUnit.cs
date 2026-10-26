using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Units
{
    internal class ReqiestOnFindingUnit
    {
        public BaseUnit unitThatLooking { get; set; } = null!;
        public List<BaseUnit> allUnits { get; set; } = null!;

        public bool isLookingForComrades { get; set; }
        
        public StrategysToFind strategysToFind { get; set; }

        public int rangeOfFindings { get; set; }

    }
}
