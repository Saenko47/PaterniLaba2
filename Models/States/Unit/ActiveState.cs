using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.States.Unit
{
    internal class ActiveState:IUnitState
    {
        public UnitStateEnum state => UnitStateEnum.Active;

        public bool MakeMove(TableContext context, BaseUnit unit) 
        {
            if (context == null || unit == null || unit.behaviour == null) throw new Exception("null context or unit");

            var req = new RequestOnHighlihtUnit()
            { position = unit.Position, highlightType = HighlightType.ActiveUnit };

            UpdateRender.Instance.Render(context.armies,new List<RequestOnHighlihtUnit> { req });

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
