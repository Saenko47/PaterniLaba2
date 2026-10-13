using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Attack : IAttack
    {
       

        public void AttackUnit(BaseUnit cellWithUnit, BaseUnit targetCell) 
        {
            if (targetCell == null || cellWithUnit == null) return;
            

            int damage = cellWithUnit.weapon.damage;

            targetCell.TakeDamage(damage);
        }
    }
}
