using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IMove
    {
        void MoveUp(BaseUnit unit);
        void MoveLeft(BaseUnit unit);
        void MoveRight(BaseUnit unit);
        void MoveDown(BaseUnit unit);
    }
}
