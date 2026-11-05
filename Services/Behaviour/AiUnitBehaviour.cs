using PaterniLab2.Services.Behaviour.States;
using PaterniLab2.Services.Behaviour.States.AI;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Interfaces.Commands;

namespace PaterniLab2.Services.Behaviour
{
    internal class AiUnitBehaviour:IBehaviour
    {
        private readonly UnitCommand _moveState;
        private readonly UnitCommand _attackState;
        private readonly UnitCommand _castingSpellState;
        private readonly IFindUnit _find;

        public AiUnitBehaviour(IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
            _moveState = new AiMoveCommand(move);
            _attackState = new AiAttackCommand(attack);
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
