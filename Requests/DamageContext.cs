using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class DamageContext
    {
        public int damage { get; set; }
        public Action<BaseUnit> OnUnitDied { get; set; } = null!;
    }
}
