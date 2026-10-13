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
                // На всякий случай пропускаем самого себя
                if (enemy == unitThatLookingForPray) continue;

                // Вычисляем расстояние по сетке (Манхэттенское расстояние)
                int distance = Math.Abs(enemy.x - startX) + Math.Abs(enemy.y - startY);

                // Проверяем: укладывается ли в радиус оружия и ближе ли текущего найденного
                if (distance <= range && distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy; // Вернет null, никого нет в радиусе атаки
        }
    }
}
