using PaterniLab2.Interfaces.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces.Factory.Units
{
    internal interface ICreateUnitState
    {
        IUnitState CreateState(UnitStateEnum unitStateEnum);
    }
}
