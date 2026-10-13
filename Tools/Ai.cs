using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Ai : IAi
    {
        private readonly IAttack _attack;
        private readonly IMove _move;
        private readonly IFindToAttack _findToAttack;

        public Ai(IAttack attack, IMove move, IFindToAttack findToAttack)
        {
            _attack = attack;
            _move = move;
            _findToAttack = findToAttack;
        }

        private void MoveTowardsEnemy(BaseUnit unit, List<BaseUnit> enemyUnits, Table table)
        {
            if (unit == null || enemyUnits == null || enemyUnits.Count == 0) return;

           
            BaseUnit target = null;
            int minDistance = int.MaxValue;

            foreach (var enemy in enemyUnits)
            {
                int dist = Math.Abs(enemy.x - unit.x) + Math.Abs(enemy.y - unit.y);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    target = enemy;
                }
            }

            if (target == null) return;

            
            var moveService = new Move(table);

            
            int diffX = target.x - unit.x;
            int diffY = target.y - unit.y;

          
            if (Math.Abs(diffX) > Math.Abs(diffY))
            {
                if (diffX > 0) moveService.MoveRight(unit);
                else moveService.MoveLeft(unit);
            }
            else
            {
                if (diffY > 0) moveService.MoveUp(unit); 
                else moveService.MoveDown(unit);
            }
        }

        public void MakeMove(BaseUnit unit, Table board, List<BaseUnit> enemyUnits, Action<BaseUnit> onUnitDeath)
        {
            if (unit == null) return;

           
            BaseUnit targetEnemy = _findToAttack.FindToAttack(unit, enemyUnits);

            if (targetEnemy != null)
            {
                
                _attack.AttackUnit(unit, targetEnemy);

                if (targetEnemy.health <= 0)
                {
                    onUnitDeath?.Invoke(targetEnemy);
                }
            }
            else
            {
               
                MoveTowardsEnemy(unit, enemyUnits, board);
            }
        }
    }
}
