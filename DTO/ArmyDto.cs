using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class ArmyDto
    {
        public string Race { get; set; }
        public bool IsLeaderAlive { get; set; }
        public bool IsAi { get; set; }
        public int CurrentIndex { get; set; }
        public List<UnitDto> Units { get; set; } = new();
        public List<UnitDto> UnitsToMoveLater { get; set; } = new();
    }
}
