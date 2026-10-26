using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class ControlledByHumanUnit:IBehaviour
    {
        protected readonly BaseUnit _owner;

        private readonly IMoveInputSource _input;
        private readonly IMoveHandler _move;
        private readonly IAttack _attack;

        private readonly IFindUnit _findToAttack;

        private int amountOfMoves;
        private int amountOfHit;


        public ControlledByHumanUnit(BaseUnit owner, IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
           
            _move = move;
            _attack = attack;
            _findToAttack = findToAttack;
            _owner = owner;
        }

        private void InitializeAmountOfMoves() 
        {
            amountOfMoves = _owner.amountOfMoves;
            amountOfHit = _owner.amountOfBattleMoves;
        }



        private bool TryMoveUnit(CombatContext context)
        {
            if (amountOfMoves <= 0) return false;

            // Делегируем выбор движения промежуточному классу, 
            // передавая ему оригинальный чистый _moveHandler
            bool success = _move.Move(context.framesOfTable, context.isCellFree, context.onMove);

            if (success)
            {
                amountOfMoves--;
            }

            return success;
        }

        private bool TryAttack(CombatContext context)
        {
            if (amountOfHit <= 0)
            {
                Console.WriteLine("Недостаточно очков боя для атаки!");
                return false;
            }

            // 1. Ищем доступные цели в радиусе оружия (точно так же, как у ИИ)
            List<BaseUnit> units = _findToAttack.Find(new ReqiestOnFindingUnit
            {
                strategysToFind = _owner.weapon.strategysToFind,
                allUnits = context.allUnits,
                isLookingForComrades = false,
                rangeOfFindings = _owner.weapon.range,
                unitThatLooking = _owner
            });

            if (units == null || units.Count == 0)
            {
                Console.WriteLine("Нет доступных целей в радиусе атаки оружия!");
                return false;
            }

            // 2. Предлагаем игроку выбрать цель из найденных
            Console.WriteLine("Выберите цель для атаки:");
            for (int i = 0; i < units.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Враг [X:{units[i].x}, Y:{units[i].y}] (Здоровье: {units[i].health})");
            }

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > units.Count)
            {
                Console.WriteLine("Неверный выбор цели.");
                return false;
            }

            var targetEnemy = units[choice - 1];

            // 3. Наносим удар через сервис _attack
            _attack.AttackUnit(_owner, targetEnemy);
            amountOfHit--; // Списываем очко боя

            Console.WriteLine($"Успешная атака по врагу! Осталось очков боя: {amountOfHit}");

            // 4. Проверяем, умер ли враг (как в ИИ)
            if (targetEnemy.health <= 0)
            {
                context.onUnitDeath?.Invoke(targetEnemy);
                Console.WriteLine("Враг уничтожен!");
            }

            return true;
        }

        private bool TryUseSkill(CombatContext context) 
        {
            throw new NotImplementedException();
        }

        private bool TryProccesMove(int decision, CombatContext context) 
        {
            switch (decision)
            {
                case 1:
                    return TryMoveUnit(context);
                case 2:
                    return TryAttack(context);
                case 3:
                    return TryUseSkill(context);
                default:
                    return false;
            }
           
        } 

       
        public void MakeMove(CombatContext context)
        {
            if (context == null) return;

            InitializeAmountOfMoves();

            while (amountOfMoves > 0 && amountOfHit > 0)
            {
               
                Console.WriteLine($"\nОчки хода: {amountOfMoves}, Очки боя: {amountOfHit}");
                Console.WriteLine("Что вы хотите сделать?\n1. Move\n2. AttackWithWeapon\n3. Use Skill");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int decision))
                {
                    Console.WriteLine("Неверный формат ввода. Попробуйте снова.");
                    continue;
                }

                if (decision < 1 || decision > 3)
                {
                    Console.WriteLine("Недопустимая команда. Выберите от 1 до 3.");
                    continue;
                }

                // Пытаемся выполнить действие
                if (!TryProccesMove(decision, context))
                {
                    Console.WriteLine("Действие недоступно или не удалось выполнить. Попробуйте снова. Или хотите закончить?\n 1 - нет\n любое другое число - да");
                    if (int.Parse(Console.ReadLine()) != 1) return;

                    
                }
            }

            Console.WriteLine("Ход юнита завершен.");
        }
    }
}
