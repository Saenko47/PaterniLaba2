using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States
{
    internal class AiAttackState:IFSNState
    {

      
        private readonly IAttack _attack;
      

        public AiAttackState(IAttack attack)
        {
            _attack = attack;
        }

        public ResultOfAction Enter(TableContext context, ActionContext actionContext)
        {
            if (actionContext.target == null || actionContext.target.Count == 0)
                return ResultOfAction.FailToAttack;

            var attackRes = 
                _attack.AttackUnit(actionContext.owner, actionContext.target[0])? 
                ResultOfAction.SuccessAction : ResultOfAction.FailToAttack;

            actionContext.owner.DecreamentMoves(attackRes);

            return attackRes;
        }

      
    }
}
