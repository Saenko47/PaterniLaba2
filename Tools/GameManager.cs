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
        private readonly Table _table;
        private readonly IMove _move;
        private readonly IFindToAttack _attack;
        private readonly IRender _render;

        private Dictionary<Race, int> raceCounts = new();

        private Race raceA;
        private Race raceB;

        private bool leaderAAlive = true;
        private bool leaderBAlive = true;

        public GameManager(Table table, IMove move, IFindToAttack attack, IRender render)
        {
            _table = table;
            _move = move;
            _attack = attack;
            _render = render;
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
                _render.RenderTable(_table);

                // Обрабатываем ход каждого живого юнита на поле
                ProcessAllUnitsTurns();

                // Проверяем состояние после ходов (вдруг кто-то умер или лидер погиб)
                if (EndGame()) break;

                Console.WriteLine("Нажмите любую клавишу для следующего хода...");
                Console.ReadKey();
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
                    if (cell?.placeForUnit != null && cell.placeForUnit.health > 0)
                    {
                        unitCells.Add((x, y, cell.placeForUnit));
                    }
                }
            }

            foreach (var (x, y, unit) in unitCells)
            {
                var currentCell = _table._board[x, y];
                if (currentCell?.placeForUnit != unit || unit.health <= 0) continue;

                ProcessMove(x, y);

                // Обновляем состояние доски сразу после каждого действия (удаление трупов и проверка лидеров)
              
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

        private void ProcessMove(int x, int y)
        {
            if (x < 0 || x >= _table.xLen) return;
            if (y < 0 || y >= _table.yLen) return;

            var cell = _table._board[x, y];
            if (cell == null || cell.placeForUnit == null || cell.placeForUnit.health <= 0) return;

            // 1. ИИ пытается найти врага и ударить его, используя реальный урон оружия
            var targetEnemy = _attack.FindToAttack(cell, _table._board);
            
            if (targetEnemy.Item1 != null)
            {
                // Берем урон из оружия атакующего юнита
                int weaponDamage = cell.placeForUnit.weapon?.damage ?? 0;
                targetEnemy.Item1.TakeDamage(weaponDamage);

                Console.WriteLine($"{cell.placeForUnit.race} атакует {targetEnemy.Item1.race} с уроном {weaponDamage}. Здоровье врага: {targetEnemy.Item1.health}");
                // Если враг погиб от удара, сразу очищаем поле и проверяем лидеров
                if (targetEnemy.Item1.health <= 0)
                {
                    Console.WriteLine($"{targetEnemy.Item1.race} погиб!");
                    raceCounts[targetEnemy.Item1.race]--;
                    if (targetEnemy.Item1.isLeader)
                    {
                        if (targetEnemy.Item1.race == raceA)
                            leaderAAlive = false;
                        else if (targetEnemy.Item1.race == raceB)
                            leaderBAlive = false;
                    }
                    // Очищаем поле от мертвого юнита
                    _table._board[targetEnemy.Item2.Item1, targetEnemy.Item2.Item2].placeForUnit = null;

                }
                return;
            }

            // 2. Если врагов рядом нет — совершаем шаг в сторону противника
            if (cell.placeForUnit.race == Tools.Enums.Race.Human)
            {
                if (x + 1 < _table.xLen && _table._board[x + 1, y]?.placeForUnit == null)
                    _move.MoveRight(cell);
                else if (y + 1 < _table.yLen && _table._board[x, y + 1]?.placeForUnit == null)
                    _move.MoveUp(cell);
            }
            else
            {
                if (x - 1 >= 0 && _table._board[x - 1, y]?.placeForUnit == null)
                    _move.MoveLeft(cell);
                else if (y - 1 >= 0 && _table._board[x, y - 1]?.placeForUnit == null)
                    _move.MoveDown(cell);
            }
        }
    }
}
