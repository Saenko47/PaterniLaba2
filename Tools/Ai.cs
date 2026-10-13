using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Ai : IAi
    {
  
        private readonly IAttack _attack;
        private readonly IMove _move;

        private readonly IFindToAttack _findToAttack;

        public Ai(IAttack attack, IMove move, IFindToAttack findToAttack)
        {
            _attack = attack;
            _move = move;
            _findToAttack = findToAttack;
        }
        private void MoveUnit(Cell cellWithUnit, Table table)
        {
            if (cellWithUnit?.placeForUnit == null) return;

            var _random = new Random();

            var moveService = new Move(table);

            // Массив делегатов с доступными направлениями движения
            Action<Cell>[] possibleMoves = new Action<Cell>[]
            {
            moveService.MoveUp,
            moveService.MoveDown,
            moveService.MoveLeft,
            moveService.MoveRight
            };

            // Выбираем случайный индекс от 0 до 3
            int randomIndex = _random.Next(possibleMoves.Length);

            // Вызываем выбранный метод перемещения
            possibleMoves[randomIndex](cellWithUnit);
        }

        public void MakeMove(Cell cellWithUnit, Table board, Action<Cell> onUnitDeath)
        {
            if (cellWithUnit == null || cellWithUnit.placeForUnit == null) return;

            IFindToAttack findToAttack = new FindEnemyToAttack();
            Cell targetCell = findToAttack.FindToAttack(cellWithUnit, board);

            if (targetCell != null)
            {
                _attack.AttackUnit(cellWithUnit, targetCell);

                // Проверяем гибель цели
                if (targetCell.placeForUnit != null && targetCell.placeForUnit.health <= 0)
                {
               
                    onUnitDeath?.Invoke(targetCell);
                }
            }
            else
            {
                MoveUnit(cellWithUnit, board);
            }
        }
    }
}
