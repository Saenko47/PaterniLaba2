using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class AttackContext
    {
      
        public BaseUnit attacker { get; set; } = null!;
        public BaseUnit defender { get; set; } = null!;
        public Action<BaseUnit> OnUnitDied = null!;
    }
}
