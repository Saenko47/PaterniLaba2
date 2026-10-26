using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class AiUnitBehaviour:IBehaviour
    {
        protected readonly BaseUnit _owner;
        private readonly IMoveHandler _move;
        private readonly IAttack _attack;

        private readonly IFindUnit _findToAttack;

        public AiUnitBehaviour(BaseUnit owner, IMoveHandler move, IAttack attack, IFindUnit findToAttack) 
        {
            _move = move;
            _attack = attack;
            _findToAttack = findToAttack;
            _owner = owner;
        }

        public void MakeMove(CombatContext context) 
        {
            int moveAmount = _owner.amountOfMoves;
            int amountOfHit = _owner.amountOfBattleMoves;

            while (amountOfHit > 0 || moveAmount > 0) 
            {
                List<BaseUnit> units = _findToAttack.Find(new ReqiestOnFindingUnit
                {
                    strategysToFind = _owner.weapon.strategysToFind,
                    allUnits = context.allUnits,
                    isLookingForComrades = false,
                    rangeOfFindings = _owner.weapon.range,
                    unitThatLooking = _owner
                }
                );

               

                if (units.Count > 0 && amountOfHit > 0)
                {
                    var targetEnemy = units[0];
                    _attack.AttackUnit(_owner, targetEnemy);
                    amountOfHit--;
                    if (targetEnemy.health <= 0)
                    {
                        context.onUnitDeath?.Invoke(targetEnemy);
                    }
                }
                else if (moveAmount > 0)
                {
                    _move.Move(context.framesOfTable, context.isCellFree, context.onMove);
                    moveAmount--;
                }
                else
                {
                  
                    break;
                }
            }
        }
    }
}
