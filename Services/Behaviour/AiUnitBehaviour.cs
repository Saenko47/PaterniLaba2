using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour
{
    //internal class AiUnitBehaviour:IBehaviour
    //{
     
    //    private readonly IMoveHandler _move;
    //    private readonly IAttack _attack;

    //    private readonly IFindUnit _findToAttack;

    //    public AiUnitBehaviour(IMoveHandler move, IAttack attack, IFindUnit findToAttack) 
    //    {
    //        _move = move;
    //        _attack = attack;
    //        _findToAttack = findToAttack;
          
    //    }

    //    public bool MakeMove(TableContext context, BaseUnit _owner)
    //    {
    //        int moveAmount = _owner.amountOfMoves;
    //        int amountOfHit = _owner.amountOfBattleMoves;
    //        bool hasMovedOrAttacked = false;

    //        while (amountOfHit > 0 || moveAmount > 0)
    //        {
    //            List<BaseUnit> units = _findToAttack.Find(new ReqiestOnFindingUnit
    //            {
    //                strategysToFind = _owner.weapon.strategysToFind,
    //                armies = context.armies,
    //                isLookingForComrades = false,
    //                rangeOfFindings = _owner.weapon.range,
    //                unitThatLooking = _owner
    //            });

    //            if (units.Count > 0 && amountOfHit > 0)
    //            {
    //                var targetEnemy = units[0];
    //                _attack.AttackUnit(_owner, targetEnemy);
                    
    //                amountOfHit--;
    //                hasMovedOrAttacked = true;

    //                if (targetEnemy.health <= 0)
    //                {
    //                    context.onUnitDeath?.Invoke(targetEnemy);
    //                }
    //            }
    //            else if (moveAmount > 0)
    //            {
    //                bool isCanMove = _move.Move(_owner, context.framesOfTable, context.isCellFree, context.onMove);
    //                if (isCanMove)
    //                {
    //                    --moveAmount;
    //                    hasMovedOrAttacked = true;
    //                }
    //                else
    //                {
                        
    //                    break;
    //                }
    //            }
    //            else
    //            {
                   
    //                break;
    //            }
    //        }

    //        return hasMovedOrAttacked;
    //    }
    //}
}
