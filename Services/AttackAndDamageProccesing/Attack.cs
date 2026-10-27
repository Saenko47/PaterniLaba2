using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
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


        public bool AttackUnit(BaseUnit cellWithUnit, BaseUnit targetCell) 
        {
            if (targetCell == null || cellWithUnit == null) return false;
            

            int damage = _damageCalculator.CalculateDamage(cellWithUnit, targetCell);

            targetCell.TakeDamage(damage);

            return true;
        }
    }
}
