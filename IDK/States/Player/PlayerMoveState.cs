using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States.Player
{
    internal class PlayerMoveState:IFSNState
    {
        private readonly IMoveHandler _move;

        public PlayerMoveState(IMoveHandler move)
        {
            _move = move;
        }

        public ResultOfAction Enter(TableContext context, ActionContext actionContext)
        {
            var moveResult = _move.Move(actionContext.owner, context.framesOfTable, context.isCellFree, context.onMove) ?
                ResultOfAction.SuccessMove : ResultOfAction.FailToMove;
            actionContext.owner.DecreamentMoves(moveResult);
            return moveResult;
        }
    }
}
