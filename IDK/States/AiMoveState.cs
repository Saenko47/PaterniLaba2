using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States
{
    internal class AiMoveState:IFSNState
    {
       
        private readonly IMoveHandler _move;

        public AiMoveState(IMoveHandler move)
        {
            _move = move;
        }

        public bool Enter(TableContext context, ActionContext actionContext)
        {
           return _move.Move(actionContext.owner, context.framesOfTable, context.isCellFree, context.onMove);
        }

        



    }
}
