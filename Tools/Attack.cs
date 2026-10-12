using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Attack : IAttack
    {
       

        public void AttackUnit(Cell cellWithUnit, Cell targetCell) 
        {
            if (targetCell == null || cellWithUnit == null) return;
            

            int damage = cellWithUnit.placeForUnit.weapon.damage;

            targetCell.placeForUnit.TakeDamage(damage);
        }
    }
}
