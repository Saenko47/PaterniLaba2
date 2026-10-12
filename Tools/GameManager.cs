using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class GameManager
    {
        public event Action<BaseUnit>? OnSomeoneDie;

        private readonly Table _table;
        private readonly IMove _move;
        private readonly IFindToAttack _attack;
        private readonly IRender _render;

        private readonly IAi ai;

        private Dictionary<Race, int> raceCounts = new();

        private Race raceA;
        private Race raceB;

        private bool leaderAAlive = true;
        private bool leaderBAlive = true;

        public GameManager(Table table, IMove move, IFindToAttack attack, IRender render, IAi ai)
        {
            _table = table;
            _move = move;
            _attack = attack;
            _render = render;
            this.ai = ai;

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
                    raceMeleeAmount = 2,
                    raceRangeAmount = 0
                },
                raceB = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 4,
                    raceRangeAmount = 0
                }
            };
            var res = _table.FirstPlacementOfUnits(req);

            raceA = req.raceA.race;
            raceB = req.raceB.race;

            raceCounts[raceA] = res.raceA.Count;
            raceCounts[raceB] = res.raceB.Count;


            ShowUnitsBeforeFigth(res.raceA);
            ShowUnitsBeforeFigth(res.raceB);
        }

        public void StartGame()
        {
            InitiaizeTabe();

            while (!EndGame())
            {
                foreach (var (r, count) in raceCounts)
                {
                    Console.WriteLine($"Remaining {r}: {count}");
                }

                _render.RenderTable(_table);

                // Обрабатываем ход каждого живого юнита на поле
                ProcessAllUnitsTurns();

                // Проверяем состояние после ходов (вдруг кто-то умер или лидер погиб)
                if (EndGame()) break;

               
            }

            _render.RenderTable(_table);
            EndGame();
        }







        private void ProcessAllUnitsTurns()
        {
            var unitCells = new List<(int x, int y, BaseUnit unit)>();

            for (int x = 0; x < _table.xLen; x++)
            {
                for (int y = 0; y < _table.yLen; y++)
                {
                    var cell = _table._board[x, y];
                    // Добавляем проверку, что юнит действительно существует и его здоровье > 0
                    if (cell?.placeForUnit != null && cell.placeForUnit.health > 0)
                    {
                        unitCells.Add((x, y, cell.placeForUnit));
                    }
                }
            }

            foreach (var (x, y, unit) in unitCells)
            {
                var currentCell = _table._board[x, y];
                // Если юнит за время чьего-то чужого хода уже умер или сдвинулся — пропускаем
                if (currentCell?.placeForUnit == null || currentCell.placeForUnit != unit || unit.health <= 0)
                    continue;

                ProcessMove(currentCell);
            }
        }

        private bool EndGame()
        {
           
            

            if (!leaderAAlive)
            {
                Console.WriteLine("Лидер Людей погиб! Орки победили!");
                return true;
            }
            else if (!leaderBAlive)
            {
                Console.WriteLine("Лидер Орков погиб! Люди победили!");
                return true;
            }
            else if (raceCounts[raceA] <= 0)
            {
                Console.WriteLine("Все Люди погибли! Орки победили!");
                return true;
            }
            else if (raceCounts[raceB] <= 0)
            {
                Console.WriteLine("Все Орки погибли! Люди победили!");
                return true;
            }
           
            return false;
        }

        private void ProcessMove(Cell cellWithUnit)
        {
            if (cellWithUnit.x < 0 || cellWithUnit.x >= _table.xLen) return;
            if (cellWithUnit.y < 0 || cellWithUnit.y >= _table.yLen) return;

           

            ai.MakeMove(cellWithUnit, _table, OnSomeoneDie);
        }

        private void HandleUnitDeath(BaseUnit deadUnit)
        {
            if (deadUnit == null) return;

            if (!raceCounts.ContainsKey(deadUnit.race))
            {
                raceCounts[deadUnit.race] = 0;
            }

            raceCounts[deadUnit.race]--;

            if (deadUnit.isLeader)
            {
                if (deadUnit.race == raceA)
                {
                    leaderAAlive = false;
                }
                else if (deadUnit.race == raceB)
                {
                    leaderBAlive = false;
                }
            }
            Console.WriteLine($"{deadUnit.race} unit has died. Remaining {deadUnit.race}: {raceCounts[deadUnit.race]}");
        }

        public void InvokeUnitDeath(BaseUnit unit)
        {
            OnSomeoneDie?.Invoke(unit);
        }
    }
}
