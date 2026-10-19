using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class DamageCalculator: IDamageCalculator
    {
        public int CalculateDamage(BaseUnit attacker, BaseUnit defender)
        {
            if (attacker == null || defender == null) return 0;
            int damage = attacker.weapon.damage;
            // You can add more complex calculations here, such as considering the attacker's and defender's stats, resistances, etc.
            return damage;
        }
    }
}
