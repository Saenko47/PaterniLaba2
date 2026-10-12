using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Move:IMove
    {
        private readonly Table _table;

        public Move(Table table)
        {
            _table = table;
        }

        public void MoveUp(Cell cellWhereUnitToMove)
        {
            // 1. Проверка выхода за верхнюю границу
            if (cellWhereUnitToMove.y + 1 >= _table.yLen) return;

            var targetCell = _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y + 1];

            // 2. Проверка: занята ли целевая ячейка
            if (targetCell.placeForUnit != null) return;

            // 3. Перемещение
            var unit = cellWhereUnitToMove.placeForUnit;
            cellWhereUnitToMove.placeForUnit = null;
            targetCell.placeForUnit = unit;
        }

        public void MoveDown(Cell cellWhereUnitToMove)
        {
            // 1. Проверка выхода за нижнюю границу (y - 1 < 0)
            if (cellWhereUnitToMove.y - 1 < 0) return;

            var targetCell = _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y - 1];

            // 2. Проверка: занята ли целевая ячейка
            if (targetCell.placeForUnit != null) return;

            // 3. Перемещение
            var unit = cellWhereUnitToMove.placeForUnit;
            cellWhereUnitToMove.placeForUnit = null;
            targetCell.placeForUnit = unit;
        }

        public void MoveLeft(Cell cellWhereUnitToMove)
        {
            // 1. Проверка выхода за левую границу (x - 1 < 0)
            if (cellWhereUnitToMove.x - 1 < 0) return;

            var targetCell = _table._board[cellWhereUnitToMove.x - 1, cellWhereUnitToMove.y];

            // 2. Проверка: занята ли целевая ячейка
            if (targetCell.placeForUnit != null) return;

            // 3. Перемещение
            var unit = cellWhereUnitToMove.placeForUnit;
            cellWhereUnitToMove.placeForUnit = null;
            targetCell.placeForUnit = unit;
        }

        public void MoveRight(Cell cellWhereUnitToMove)
        {
            // 1. Проверка выхода за правую границу (x + 1 >= xLen)
            if (cellWhereUnitToMove.x + 1 >= _table.xLen) return;

            var targetCell = _table._board[cellWhereUnitToMove.x + 1, cellWhereUnitToMove.y];

            // 2. Проверка: занята ли целевая ячейка
            if (targetCell.placeForUnit != null) return;

            // 3. Перемещение
            var unit = cellWhereUnitToMove.placeForUnit;
            cellWhereUnitToMove.placeForUnit = null;
            targetCell.placeForUnit = unit;
        }
    }
}
