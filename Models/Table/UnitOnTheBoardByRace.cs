using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Table
{
    internal record UnitOnTheBoardByRace
    {
        public List<Army> placedArmies { get; set; } = new List<Army>();
     
    }
}
