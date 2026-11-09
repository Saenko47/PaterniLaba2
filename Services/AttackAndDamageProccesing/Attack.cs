using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.AttackAndDamageProccesing
{
    internal class Attack : IAttack
    {
        private readonly IDamageCalculator _damageCalculator;

        public Attack(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }


        public bool AttackUnit(BaseUnit attacker, List<BaseUnit> defenders) 
        {
            if (defenders == null || attacker == null) return false;

            foreach (var defender in defenders) {
                int damage = _damageCalculator.CalculateDamage(attacker, defender);
                defender.TakeDamage(damage);
            }
           
            return true;
        }
    }
}
