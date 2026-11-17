using PaterniLab2.Interfaces.Factory.Units;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.States.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories
{
    internal class BaseCreateUnitState: ICreateUnitState
    {

        public IUnitState CreateState(UnitStateEnum unitStateEnum) 
        {
            switch (unitStateEnum) 
            {
                case UnitStateEnum.Active: return new ActiveState();
                case UnitStateEnum.Delayed: return new DelayedState();
                case UnitStateEnum.Unactive: return new UnectiveState();
                default: throw new Exception("Theres no such state!!!");
            }
        
        }
    }
}
