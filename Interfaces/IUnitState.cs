using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces
{
    internal interface IUnitState
    {
        UnitStateEnum state { get; }

        bool MakeMove(TableContext context, BaseUnit unit);

        void OnEnter(BaseUnit unit);
        void OnExit(BaseUnit unit);
    }
}
