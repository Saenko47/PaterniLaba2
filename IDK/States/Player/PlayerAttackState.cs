using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States.Player
{
    internal class PlayerAttackState : IFSNState
    {
        private readonly IAttack _attack;

        public PlayerAttackState(IAttack attack)
        {
            _attack = attack;
        }

        private void HighlightTargetUnits(TableContext context, List<BaseUnit> units, HighlightType highlightType)
        {
            var highlightRequests = new List<RequestOnHighlihtUnit>();
            foreach (var unit in units)
            {
                highlightRequests.Add(new RequestOnHighlihtUnit
                {
                    position = unit.Position,
                    highlightType = highlightType
                });
            }
            context.updateRender(highlightRequests);
        }

        private void ShowUnitsToAttack(List<BaseUnit> targets)
        {
            if (targets == null || targets.Count == 0) return;

            Console.WriteLine("\n=== Доступные цели для атаки ===");
            for (int k = 0; k < targets.Count; k++)
            {
                
                Console.WriteLine($"{k + 1}. {targets[k].race} на позиции {targets[k].Position} (HP: {targets[k].health})");
            }
        }

        public ResultOfAction Enter(TableContext context, ActionContext actionContext)
        {
            if (actionContext.target == null || actionContext.target.Count == 0)
            {
                Console.WriteLine("Нет доступных целей для атаки!");
                return ResultOfAction.FailToAttack;
            }

           
            ShowUnitsToAttack(actionContext.target);
            HighlightTargetUnits(context, actionContext.target, HighlightType.TargetUnit);

            Console.Write("Выберите номер цели: ");

            
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int selectedTargetIndex) || selectedTargetIndex < 1 || selectedTargetIndex > actionContext.target.Count)
            {
                Console.WriteLine("Неверный выбор цели!");
                return ResultOfAction.FailToAttack;
            }

           
            var targetEnemy = actionContext.target[selectedTargetIndex - 1];

            bool isSuccess = _attack.AttackUnit(actionContext.owner, targetEnemy);
            var attackRes = isSuccess ? ResultOfAction.SuccessAction : ResultOfAction.FailToAttack;

            actionContext.owner.DecreamentMoves(attackRes);

          
            if (targetEnemy.health <= 0)
            {
                context.onUnitDeath?.Invoke(targetEnemy);
                Console.WriteLine($"Враг {targetEnemy.race} уничтожен!");
            }

            return attackRes;
        }
    }
}
