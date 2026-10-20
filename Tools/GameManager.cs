using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class GameManager:IGameManager
    {
        public event Action<BaseUnit>? OnSomeoneDie;

        private readonly Table _table;
        private readonly IMove _move;
        private readonly IFindToAttack _attack;
        private readonly IRender _render;

        private List<BaseUnit> unitsA = new List<BaseUnit>();
        private List<BaseUnit> unitsB = new List<BaseUnit>();

        private bool leaderAAlive = true;
        private bool leaderBAlive = true;

        public GameManager(Table table, IMove move, IFindToAttack attack, IRender render)
        {
            _table = table;
            _move = move;
            _attack = attack;
            _render = render;
            _ai = ai;

            OnSomeoneDie += HandleUnitDeath;
        }

        private void ShowUnitsBeforeFigth(List<BaseUnit> units)
        {
            foreach (var unit in units)
            {
                Console.WriteLine(unit);
            }
        }

        private void InitiaizeTabe()
        {
            var req = new Requests.Table.CreateGameRequest
            {
                raceA = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Race.Human,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 3,
                    raceRangeAmount = 0
                },
                raceB = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 2,
                    raceRangeAmount = 0
                }
            };
            var res = _table.FirstPlacementOfUnits(req);

            unitsA = res.raceA;
            unitsB = res.raceB;

            ShowUnitsBeforeFigth(res.raceA);
            ShowUnitsBeforeFigth(res.raceB);
        }

        public void StartGame()
        {
            InitiaizeTabe();

            int indexA = 0;
            int indexB = 0;

            while (true)
            {
                
                if (CheckGameOver(out string winMessage))
                {
                    Console.WriteLine(winMessage);
                    break;
                }

                if (unitsA.Count > 0 && unitsB.Count > 0)
                {
                    Console.WriteLine($"Units of {unitsA[0].race}: {unitsA.Count}");
                    Console.WriteLine($"Units of {unitsB[0].race}: {unitsB.Count}");
                }

                
                _render.RenderTable(_table, unitsA.Concat(unitsB).ToList());

               
                Console.WriteLine("\nНажмите Enter, чтобы сделать ход следующим юнитом...");
                Console.ReadLine();

               
                if (unitsA.Count() > 0)
                {
                    indexA = indexA % unitsA.Count();
                    var unitA = unitsA[indexA];
                    if (unitA.health > 0)
                    {
                        ProcessMove(unitA);
                    }
                    indexA++;
                }

                
                if (CheckGameOver(out string winMsgA))
                {
                    _render.RenderTable(_table, unitsA.Concat(unitsB).ToList());
                    Console.WriteLine(winMsgA);
                    break;
                }

               
                if (unitsB.Count() > 0)
                {
                    indexB = indexB % unitsB.Count;
                    var unitB = unitsB[indexB];
                    if (unitB.health > 0)
                    {
                        ProcessMove(unitB);
                    }
                    indexB++;
                }
            }

            
            _render.RenderTable(_table, unitsA.Concat(unitsB).ToList());
        }

        private bool CheckGameOver(out string message)
        {
            if (!leaderAAlive)
            {
                message = "Лидер Людей погиб! Орки победили!";
                return true;
            }
            if (!leaderBAlive)
            {
                message = "Лидер Орков погиб! Люди победили!";
                return true;
            }
            if (unitsA.Count() <= 0)
            {
                message = "Все Люди погибли! Орки победили!";
                return true;
            }
            if (unitsB.Count() <= 0)
            {
                message = "Все Орки погибли! Люди победили!";
                return true;
            }

            message = string.Empty;
            return false;
        }

        private void ProcessMove(BaseUnit unit)
        {
            Console.WriteLine($"Ходит юнит: {unit.race} на позиции ({unit.x}, {unit.y})");
            var opponentTeam = unit.race == unitsA[0].race ? unitsB : unitsA;
            unit.MakeMove(opponentTeam, OnSomeoneDie);
        }

        private void HandleUnitDeath(BaseUnit deadUnit)
        {
            if (deadUnit == null) return;

            // Очищаем клетку на карте
            _table.ClearCell(deadUnit.x, deadUnit.y);

            if (deadUnit.race == unitsA[0].race)
            {
                unitsA.Remove(deadUnit);
            }
            else
            {
                unitsB.Remove(deadUnit);
            }

            if (deadUnit.isLeader)
            {
                if (deadUnit.race == unitsA[0].race)
                {
                    leaderAAlive = false;
                }
                else
                {
                    leaderBAlive = false;
                }
            }

            Console.WriteLine($"{deadUnit.race} unit has died.");
        }

        public void InvokeUnitDeath(BaseUnit deadUnit)
        {
            OnSomeoneDie?.Invoke(deadUnit);
        }
    }
}
