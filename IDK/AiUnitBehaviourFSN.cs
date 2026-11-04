using PaterniLab2.IDK.States;
using PaterniLab2.IDK.States.AI;
using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK
{
    internal class AiUnitBehaviourFSN:IBehaviour
    {
        private readonly IFSNState _moveState;
        private readonly IFSNState _attackState;
        private readonly IFSNState _castingSpellState;
        private readonly IFindUnit _find;

        public AiUnitBehaviourFSN(IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
            _moveState = new AiMoveState(move);
            _attackState = new AiAttackState(attack);
            _find = findToAttack;
        }




        public bool MakeMove(TableContext context, BaseUnit owner)
        {
            var actionResult = ResultOfAction.Awaiting;
            bool check = false;
            while (owner.isCanMakeSomething)
            {
               check = true;
                List<BaseUnit> units = _find.Find(new ReqiestOnFindingUnit
                {
                    strategysToFind = owner.weapon.strategysToFind,
                    armies = context.armies,
                    isLookingForComrades = false,
                    rangeOfFindings = owner.weapon.range,
                    unitThatLooking = owner
                });
                Console.WriteLine($"Units found: {units.Count}");

                if (units != null && units.Count > 0 && owner.isCanAttack)
                {
                    var actionContext = new ActionContext
                    {
                        owner = owner,
                        target = units
                    };
                    actionResult = _attackState.Enter(context, actionContext);
                }
                else if (owner.isCanMove) 
                {
                    var moveActionContext = new ActionContext
                    {
                        owner = owner,
                        target = null
                    };
                    actionResult = _moveState.Enter(context, moveActionContext);
                }
                else
                {
                   
                    break;
                }

                if (actionResult == ResultOfAction.FailToMove || actionResult == ResultOfAction.FailToAttack)
                {
                    break;
                }
            }
            Console.WriteLine("Check: " + check);

            return actionResult == ResultOfAction.SuccessAction || actionResult == ResultOfAction.SuccessMove;
        }

    }
}
