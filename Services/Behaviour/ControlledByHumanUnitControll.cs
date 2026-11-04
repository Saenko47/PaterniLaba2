using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.States.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour
{
    internal class ControlledByHumanUnit:IBehaviour
    {
      

        private readonly IMoveHandler _move;
        private readonly IAttack _attack;

        private readonly IFindUnit _findToAttack;

        private int amountOfMoves;
        private int amountOfHit;


        public ControlledByHumanUnit( IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
           
            _move = move;
            _attack = attack;
            _findToAttack = findToAttack;
           
        }

        private void InitializeAmountOfMoves(BaseUnit _owner) 
        {
            amountOfMoves = _owner.amountOfMoves;
            amountOfHit = _owner.amountOfBattleMoves;
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



        private bool TryMoveUnit(TableContext context, BaseUnit _owner)
        {
            if (amountOfMoves <= 0) return false;

          
            bool success = _move.Move(_owner,context.framesOfTable, context.isCellFree, context.onMove);

            if (success)
            {
                amountOfMoves--;
            }

            return success;
        }

        private bool TryAttack(TableContext context, BaseUnit _owner)
        {
            if (amountOfHit <= 0)
            {
                Console.WriteLine("Недостаточно очков боя для атаки!");
                return false;
            }

            List<BaseUnit> units = _findToAttack.Find(new ReqiestOnFindingUnit
            {
                strategysToFind = _owner.weapon.strategysToFind,
                armies = context.armies,
                isLookingForComrades = false,
                rangeOfFindings = _owner.weapon.range,
                unitThatLooking = _owner
            });

            if (units == null || units.Count == 0)
            {
                Console.WriteLine("Нет доступных целей в радиусе атаки оружия!");
                return false;
            }

            Console.WriteLine("Выберите цель для атаки:");
            for (int i = 0; i < units.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Враг [X:{units[i].x}, Y:{units[i].y}] (Здоровье: {units[i].health})");
            }

           HighlightTargetUnits(context, units, HighlightType.TargetUnit);


            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > units.Count)
            {
                Console.WriteLine("Неверный выбор цели.");
                return false;
            }

            var targetEnemy = units[choice - 1];

           
            _attack.AttackUnit(_owner, targetEnemy);
           
            amountOfHit--; 

            Console.WriteLine($"Успешная атака по врагу! Осталось очков боя: {amountOfHit}");

          
            if (targetEnemy.health <= 0)
            {
                context.onUnitDeath?.Invoke(targetEnemy);
                Console.WriteLine("Враг уничтожен!");
            }

            return true;
        }

        private bool TryUseSkill(TableContext context, BaseUnit _owner) 
        {
            throw new NotImplementedException();
        }

        private bool TryProccesMove(int decision, TableContext context, BaseUnit _owner) 
        {
            switch (decision)
            {
                case 1:
                    return TryMoveUnit(context, _owner);
                case 2:
                    return TryAttack(context, _owner);
                case 3:
                    return TryUseSkill(context, _owner);
                default:
                    return false;
            }
           
        }


        public bool MakeMove(TableContext context, BaseUnit _owner)
        {
            if (context == null) return false;

            InitializeAmountOfMoves(_owner);

            while (amountOfMoves > 0 && amountOfHit > 0)
            {
                

                Console.WriteLine($"\nОчки хода: {amountOfMoves}, Очки боя: {amountOfHit}");
                Console.WriteLine("Что вы хотите сделать?\n1. Move\n2. AttackWithWeapon\n3. Use Skill\n4. Move later");


                string actionInput = Console.ReadLine();
                if (!int.TryParse(actionInput, out int decision))
                {
                    Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
                    continue;
                }

                if (decision < 1 || decision > 4)
                {
                    Console.WriteLine("Недопустимая команда. Выберите от 1 до 3.");
                    continue;
                }

                if (decision == 4) {
                    _owner.SetState(new UnectiveState());
                    return false;

                }
                
                
                if (!TryProccesMove(decision, context, _owner))
                {
                    Console.WriteLine("Действие недоступно или не удалось выполнить. Попробуйте снова. Или хотите закончить?\n 1 - нет\n любое другое число (или Enter) - да");

                    string retryInput = Console.ReadLine();

                   
                    if (string.IsNullOrEmpty(retryInput) || !int.TryParse(retryInput, out int choice) || choice != 1)
                    {
                        break; 
                    }

                    Console.Clear();

                   
                }

                context.updateRender(null);
            }

            Console.WriteLine("Ход юнита завершен.");
            return true;
        }
    }
}
