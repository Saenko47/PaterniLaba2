using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.States.Unit
{
    internal class UnectiveState:IUnitState
    {
        public UnitStateEnum state => UnitStateEnum.Unactive;

        public bool MakeMove(TableContext context, BaseUnit unit)
        {
            Console.WriteLine("Unit is unective!!!");
            return false;

        }

        public void OnEnter(BaseUnit unit) { }
        public void OnExit(BaseUnit unit) { }
    }
}
