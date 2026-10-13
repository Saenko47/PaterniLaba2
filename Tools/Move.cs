using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
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

        public void MoveUp(BaseUnit unit)
        {
            if(unit == null) return;
            // 1. Проверка выхода за верхнюю границу
            if (unit.y + 1 >= _table.yLen) return;



            // 2. Проверка: занята ли целевая ячейка
            if (_table._board[unit.x, unit.y + 1].isEmpty == false) return;

           _table.ClearCell(unit.x, unit.y);
            // 3. Перемещение
            unit.SetPosition(unit.x, unit.y + 1);

          _table.SetCell(unit.x, unit.y);

        }

        public void MoveDown(BaseUnit unit)
        {
            if (unit == null) return;
            // 1. Проверка выхода за нижнюю границу (y - 1 < 0)
            if (unit.y - 1 < 0) return;

            // 2. Проверка: занята ли целевая ячейка
            if (_table._board[unit.x, unit.y - 1].isEmpty == false) return;


            _table.ClearCell(unit.x, unit.y);
            // 3. Перемещение
            unit.SetPosition(unit.x, unit.y - 1);

            _table.SetCell(unit.x, unit.y);
        }

        public void MoveLeft(BaseUnit unit)
        {
            if (unit == null) return;
            // 1. Проверка выхода за левую границу (x - 1 < 0)
            if (unit.x - 1 < 0) return;

         
            // 2. Проверка: занята ли целевая ячейка
            if (_table._board[unit.x - 1, unit.y].isEmpty == false) return;

            // 3. Перемещение
            _table.ClearCell(unit.x, unit.y);
            // 3. Перемещение
            unit.SetPosition(unit.x - 1, unit.y);

            _table.SetCell(unit.x, unit.y);
        }

        public void MoveRight(BaseUnit unit)
        {
            // 1. Проверка выхода за правую границу (x + 1 >= xLen)
            if (unit.x + 1 >= _table.xLen) return;

           

            // 2. Проверка: занята ли целевая ячейка
            if (_table._board[unit.x + 1, unit.y].isEmpty == false) return;

            // 3. Перемещение
            _table.ClearCell(unit.x, unit.y);
            unit.SetPosition(unit.x + 1, unit.y);
            _table.SetCell(unit.x, unit.y);
        }
    }
}
