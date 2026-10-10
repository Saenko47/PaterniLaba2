using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal record UnitOnTheBoardByRace
    {
        public List<BaseUnit> raceA = null!;
        public List<BaseUnit> raceB = null!;
    }
}
