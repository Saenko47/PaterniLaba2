using PaterniLab2.Interfaces.Commands;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour.States.Player
{
    internal class PlayerMoveCommand:UnitCommand
    {
        private readonly IMoveHandler _move;

        public PlayerMoveCommand(IMoveHandler move)
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
