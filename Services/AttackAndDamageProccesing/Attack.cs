using PaterniLab2.Interfaces;
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


        public bool AttackUnit(BaseUnit attacker, BaseUnit defender) 
        {
            if (defender == null || attacker == null) return false;
            

            int damage = _damageCalculator.CalculateDamage(attacker, defender);

            defender.TakeDamage(damage);

            return true;
        }
    }
}
