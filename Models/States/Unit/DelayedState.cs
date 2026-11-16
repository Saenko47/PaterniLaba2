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
    internal class DelayedState: IUnitState
    {
        public UnitStateEnum state => UnitStateEnum.Delayed;

        public bool MakeMove(TableContext context, BaseUnit unit)
        {
            if (context == null || unit == null || unit.behaviour == null) throw new Exception("null context or unit");

            var req = new RequestOnHighlihtUnit()
            { position = unit.Position, highlightType = HighlightType.ActiveUnit };

            GlobalInputOutput.Instance.SetHighlight(new List<RequestOnHighlihtUnit> { req });

            bool isSucces = unit.behaviour.MakeMove(context, unit);
            if (!isSucces)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer("Do it later");

                return false;
            }
            unit.Tick();
            unit.SetState(new UnectiveState());
            return true; 


        }

        public void OnEnter(BaseUnit unit) { }
        public void OnExit(BaseUnit unit) { }
    }
}
