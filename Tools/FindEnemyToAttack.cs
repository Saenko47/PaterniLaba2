using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class FindEnemyToAttack: IFindToAttack
    {
        public BaseUnit? FindToAttack(Models.Cell unitThatLookingForPray, Models.Cell[,] board)
        {
            int startX = unitThatLookingForPray.x;
            int startY = unitThatLookingForPray.y;
            int range = unitThatLookingForPray.placeForUnit.weapon.range;

            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            // Проходим циклом по квадрату вокруг юнита в пределах радиуса оружия
            for (int x = Math.Max(0, startX - range); x <= Math.Min(rows - 1, startX + range); x++)
            {
                for (int y = Math.Max(0, startY - range); y <= Math.Min(cols - 1, startY + range); y++)
                {
                    // Пропускаем саму клетку, где стоит наш юнит
                    if (x == startX && y == startY) continue;

                    var targetCell = board[x, y];

                    // Проверяем, есть ли там юнит и враг ли он (здесь можно добавить проверку команды)
                    if (targetCell?.placeForUnit != null && targetCell?.placeForUnit.race != unitThatLookingForPray.placeForUnit.race)
                    {
                        // Возвращаем первую попавшуюся цель
                        return targetCell.placeForUnit;
                    }
                }
            }

            // Враг/цель не найдены
            return null;
        }
    }
}
