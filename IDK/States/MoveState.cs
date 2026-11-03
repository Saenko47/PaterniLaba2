using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States
{
    internal class MoveState:IUnitState
    {
        public  UnitStateEnum state => UnitStateEnum.Active;
        private readonly IMoveHandler _move;

        public MoveState(IMoveHandler move)
        {
            _move = move;
        }

        public bool MakeMove(CombatContext context, BaseUnit unit)
        {
           return _move.Move(unit, context.framesOfTable, context.isCellFree, context.onMove);
        }

        public void OnEnter(BaseUnit unit)
        {
            return;
        }

        public void OnExit(BaseUnit unit)
        {
            return;
        }



    }
}
