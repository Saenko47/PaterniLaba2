using PaterniLab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IMove
    {
        void MoveUp(Cell cellWhereUnitToMove);
        void MoveLeft(Cell cellWhereUnitToMove);
        void MoveRight(Cell cellWhereUnitToMove);
        void MoveDown(Cell cellWhereUnitToMove);
    }
}
