using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class FindEnemyToAttack: IFindToAttack
    {
        public BaseUnit FindToAttack(BaseUnit unitThatLookingForPray, List<BaseUnit> enemyUnits)
        {
            if (unitThatLookingForPray == null || enemyUnits == null) return null;

            int startX = unitThatLookingForPray.x;
            int startY = unitThatLookingForPray.y;
            int range = unitThatLookingForPray.weapon.range;

            BaseUnit closestEnemy = null;
            int minDistance = int.MaxValue;

            foreach (var enemy in enemyUnits)
            {
               
                if (enemy == unitThatLookingForPray) continue;

                
                int distance = Math.Abs(enemy.x - startX) + Math.Abs(enemy.y - startY);

               
                if (distance <= range && distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy; 
        }
    }
}
