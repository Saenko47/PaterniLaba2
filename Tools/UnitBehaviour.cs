using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class UnitBehaviour:IBehaviour
    {
        protected readonly BaseUnit _owner;
        private readonly IMoveHandler _move;
        private readonly IAttack _attack;

        private readonly IFindToAttack _findToAttack;

        public UnitBehaviour(BaseUnit owner, IMoveHandler move, IAttack attack, IFindToAttack findToAttack) 
        {
            _move = move;
            _attack = attack;
            _findToAttack = findToAttack;
            _owner = owner;
        }

        public void MakeMove(List<BaseUnit> enemyUnits, Action<BaseUnit> onUnitDeath) 
        {
            int moveAmount = _owner.amountOfMoves;
            int amountOfHit = _owner.amountOfBattleMoves;

            while (amountOfHit > 0 || moveAmount > 0) 
            {
                BaseUnit targetEnemy = _findToAttack.FindToAttack(_owner, enemyUnits);

                if (targetEnemy != null && amountOfHit > 0)
                {
                    _attack.AttackUnit(_owner, targetEnemy);
                    amountOfHit--;
                    if (targetEnemy.health <= 0)
                    {
                        onUnitDeath?.Invoke(targetEnemy);
                    }
                }
                else if (moveAmount > 0)
                {
                    _move.Move();
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
