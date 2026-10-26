using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IAttack
    {
        bool AttackUnit(BaseUnit cellWithUnit, BaseUnit targetCell);
    }
}
