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

          
            if (unit.y + 1 >= _table.yLen) return;

           
            if (_table._board[unit.x, unit.y + 1].isEmpty == false) return;

           _table.ClearCell(unit.x, unit.y);
          
            unit.SetPosition(unit.x, unit.y + 1);

          _table.SetCell(unit.x, unit.y);

        }

        public void MoveDown(BaseUnit unit)
        {
            if (unit == null) return;
           
            if (unit.y - 1 < 0) return;

           
            if (_table._board[unit.x, unit.y - 1].isEmpty == false) return;


            _table.ClearCell(unit.x, unit.y);
           
            unit.SetPosition(unit.x, unit.y - 1);

            _table.SetCell(unit.x, unit.y);
        }

        public void MoveLeft(BaseUnit unit)
        {
            if (unit == null) return;
          
            if (unit.x - 1 < 0) return;

         
          
            if (_table._board[unit.x - 1, unit.y].isEmpty == false) return;

          
            _table.ClearCell(unit.x, unit.y);
           
            unit.SetPosition(unit.x - 1, unit.y);

            _table.SetCell(unit.x, unit.y);
        }

        public void MoveRight(BaseUnit unit)
        {
            
            if (unit.x + 1 >= _table.xLen) return;

           

           
            if (_table._board[unit.x + 1, unit.y].isEmpty == false) return;

            
            _table.ClearCell(unit.x, unit.y);
            unit.SetPosition(unit.x + 1, unit.y);
            _table.SetCell(unit.x, unit.y);
        }
    }
}
