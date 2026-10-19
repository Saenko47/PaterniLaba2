using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Attack : IAttack
    {
        private readonly IDamageCalculator _damageCalculator;

        public Attack(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }


        public void AttackUnit(BaseUnit cellWithUnit, BaseUnit targetCell) 
        {
            if (targetCell == null || cellWithUnit == null) return;
            

            int damage = _damageCalculator.CalculateDamage(cellWithUnit, targetCell);

            targetCell.TakeDamage(damage);
        }
    }
}
