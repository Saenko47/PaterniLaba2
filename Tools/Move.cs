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
            // Assuming _table.yLen is the size, valid indices are 0 to yLen - 1
            if (cellWhereUnitToMove.y + 1 >= _table.yLen) return;

            var unit = cellWhereUnitToMove.placeForUnit;

            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y].placeForUnit = null;
            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y + 1].placeForUnit = unit;
        }

        public void MoveDown(Cell cellWhereUnitToMove)
        {
            if (cellWhereUnitToMove.y - 1 < 0) return;

            var unit = cellWhereUnitToMove.placeForUnit;

            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y].placeForUnit = null;
            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y - 1].placeForUnit = unit;
        }

        public void MoveLeft(Cell cellWhereUnitToMove)
        {
            if (cellWhereUnitToMove.x - 1 < 0) return;

            var unit = cellWhereUnitToMove.placeForUnit;

            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y].placeForUnit = null;
            _table._board[cellWhereUnitToMove.x - 1, cellWhereUnitToMove.y].placeForUnit = unit;
        }

        public void MoveRight(Cell cellWhereUnitToMove)
        {
            if (cellWhereUnitToMove.x + 1 >= _table.xLen) return;

            var unit = cellWhereUnitToMove.placeForUnit;

            _table._board[cellWhereUnitToMove.x, cellWhereUnitToMove.y].placeForUnit = null;
            _table._board[cellWhereUnitToMove.x + 1, cellWhereUnitToMove.y].placeForUnit = unit;
        }
    }
}
