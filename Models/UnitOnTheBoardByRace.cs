using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal record UnitOnTheBoardByRace
    {
        public List<Army> placedArmies { get; set; } = new List<Army>();
     
    }
}
