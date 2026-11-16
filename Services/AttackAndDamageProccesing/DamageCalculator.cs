using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.AttackAndDamageProccesing
{
    internal class DamageCalculator: IDamageCalculator
    {
        public int CalculateDamage(BaseUnit attacker, BaseUnit defender)
        {
            if (attacker == null || defender == null) return 0;
            int damage = attacker.damage - defender.armor;
            // will be changed to a more complex formula later
            return damage;
        }
    }
}
