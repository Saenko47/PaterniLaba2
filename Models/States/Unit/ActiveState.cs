using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.States.Unit
{
    internal class ActiveState:IUnitState
    {
        public UnitStateEnum state => UnitStateEnum.Active;

        public bool MakeMove(CombatContext context, BaseUnit unit) 
        {
            if (context == null || unit == null || unit.behaviour == null) throw new Exception("null context or unit");

            bool isSucces = unit.behaviour.MakeMove(context, unit);
            if (!isSucces) 
            {
                Console.WriteLine("Do it later");
                unit.SetState(new DelayedState());
                return false;
            }
            unit.Tick();
            unit.SetState(new UnectiveState());
            return true; ;


        }

        public void OnEnter(BaseUnit unit) { }
        public void OnExit(BaseUnit unit) { }
    }
}
