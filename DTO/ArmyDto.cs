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
         Race Race { get; set; }
         bool IsLeaderAlive { get; set; }
         bool IsAi { get; set; }
         int CurrentIndex { get; set; }
         List<BaseUnitDto> Units { get; set; } = new();

        ArmyDto() { }

     

    }
}
