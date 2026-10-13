using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IAttack
    {
        void AttackUnit(BaseUnit cellWithUnit, BaseUnit targetCell);
    }
}
