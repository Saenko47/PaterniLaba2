using PaterniLab2.Services.Behaviour.States.Player;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.States.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Interfaces.Commands;
using PaterniLab2.Singltons;
using PaterniLab2.Services.Behaviour.Commands.Player;

namespace PaterniLab2.Services.Behaviour
{
    internal class PlayerUnitBehaviour : IBehaviour
    {
        private readonly UnitCommand _moveState;
        private readonly UnitCommand _attackState;
        private readonly UnitCommand _castingSpellState;
        private readonly IFindUnit _find;

        public PlayerUnitBehaviour(IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
            _find = findToAttack;
            _moveState = new PlayerMoveCommand(move);
            _attackState = new NewPlayerAttackCommand(attack);
            _castingSpellState = new CastingSpellCommand();
            _castingSpellState = null;
        }

        private void ShowDecision()
        {
            Console.WriteLine("\nChoose action: 1 - Move, 2 - Attack, 3 - Cast Spell, 4 - End Turn");
        }

        private ResultOfAction TryMove(TableContext context, BaseUnit owner)
        {
            if (owner.isCanMove)
            {
                return _moveState.Enter(context, new ActionContext { owner = owner });
            }
            else
            {
                Console.WriteLine("You cannot move. No movement points left.");
                return ResultOfAction.FailToMove;
            }
        }

        private ResultOfAction TryAttack(TableContext context, BaseUnit owner)
        {
            if (owner.isCanAttack) // Используем проверку очков боя
            {
                
                return _attackState.Enter(context, new ActionContext { owner = owner, targetFinder = _find.Find });
            }
            else
            {
                Console.WriteLine("You cannot attack. No battle moves left.");
                return ResultOfAction.FailToAttack;
            }
        }

        private ResultOfAction TryCast(TableContext context, BaseUnit owner) 
        {
            if (owner.isCanAttack) // Используем проверку очков боя
            {

                return _castingSpellState.Enter(context, new ActionContext { owner = owner, targetFinder = _find.Find });
            }
            else
            {
                Console.WriteLine("You cannot cast. No battle moves left.");
                return ResultOfAction.FailToAttack;
            }
        }

        public bool MakeMove(TableContext context, BaseUnit owner)
        {
            var actionResult = ResultOfAction.Awaiting;

            while (owner.isCanMakeSomething)
            {
                

                
             
                ShowDecision();

                // Безопасное чтение целого числа через ReadLine
                string input = GlobalInputOutput.Instance.Read(context.armies,"Yor Choise:");
                if (!int.TryParse(input, out int decision) || decision < 1 || decision > 4)
                {
                    GlobalInputOutput.Instance.AddTextDataToBuffer("Try again");
                    GlobalInputOutput.Instance.Execute();
                    continue;
                }

                switch (decision)
                {
                    case 1:
                       
                        actionResult = TryMove(context, owner); // Исправлено: вызываем движение
                        break;
                    case 2:
                        actionResult = TryAttack(context, owner); // Исправлено: сохраняем в actionResult
                        break;
                    case 3:
                        actionResult = TryCast(context, owner);
                        break;
                    case 4:
                        owner.SetState(new UnectiveState());
                        return actionResult == ResultOfAction.SuccessAction || actionResult == ResultOfAction.SuccessMove; // End turn
                }

                if (actionResult == ResultOfAction.FailToAttack || actionResult == ResultOfAction.FailToMove)
                {
                    GlobalInputOutput.Instance.AddTextDataToBuffer("Action failed. Try another action.");
                    GlobalInputOutput.Instance.Execute();
                }
                
            }

           

            return actionResult == ResultOfAction.SuccessAction || actionResult == ResultOfAction.SuccessMove;
        }
    }
}
