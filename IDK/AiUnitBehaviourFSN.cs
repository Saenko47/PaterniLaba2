using PaterniLab2.IDK.States;
using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.IDK
{
    internal class AiUnitBehaviourFSN
    {
        private readonly IFSNState _moveState;
        private readonly IFSNState _attackState;
        private readonly IFindUnit _find;

        public AiUnitBehaviourFSN(IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
            _moveState = new AiMoveState(move);
            _attackState = new AiAttackState(attack);
            _find = findToAttack;
        }

        public bool MakeMove(TableContext context, BaseUnit owner)
        {
            if (owner == null)
                return false;

            int moveAmount = owner.amountOfMoves;
            int amountOfHit = owner.amountOfBattleMoves;
            bool hasMovedOrAttacked = false;

            while (amountOfHit > 0 || moveAmount > 0)
            {
                List<BaseUnit> foundUnits = _find.Find(new ReqiestOnFindingUnit
                {
                    strategysToFind = owner.weapon.strategysToFind,
                    armies = context.armies,
                    isLookingForComrades = false,
                    rangeOfFindings = owner.weapon.range,
                    unitThatLooking = owner
                });

                if (foundUnits != null && foundUnits.Count > 0 && amountOfHit > 0)
                {
                    var attackContext = new ActionContext
                    {
                        owner = owner,
                        target = foundUnits
                    };

                    bool attackSuccess = _attackState.Enter(context, attackContext);

                    if (attackSuccess)
                    {
                        amountOfHit--;
                        hasMovedOrAttacked = true;

                        var targetEnemy = foundUnits[0];
                        if (targetEnemy.health <= 0)
                        {
                            context.onUnitDeath?.Invoke(targetEnemy);
                        }
                    }
                    else
                    {
                        break; 
                    }
                }
              
                else if (moveAmount > 0)
                {
                    var moveContext = new ActionContext
                    {
                        owner = owner
                    };

                    bool moveSuccess = _moveState.Enter(context, moveContext);

                    if (moveSuccess)
                    {
                        moveAmount--;
                        hasMovedOrAttacked = true;
                    }
                    else
                    {
                        break; 
                    }
                }
                else
                {
                    break;
                }
            }

            return hasMovedOrAttacked;
        }

    }
}
