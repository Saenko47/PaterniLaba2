using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class GameManager
    {
        private readonly Table _table;
        private readonly IMove _move;
        private readonly IFindToAttack _attack;
        private readonly IRender _render;

        private int raceACount;
        private int raceBCount;
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
            var res = _table.FirstPlacementOfUnits(new Requests.Table.CreateGameRequest
            {
                raceA = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Tools.Enums.Race.Human,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 2,
                    raceRangeAmount = 0
                },
                raceB = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Tools.Enums.Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 4,
                    raceRangeAmount = 0
                }
            });

            UpdateCountsAndLeaders(res.raceA, res.raceB);

            ShowUnitsBeforeFigth(res.raceA);
            ShowUnitsBeforeFigth(res.raceB);
        }

        public void StartGame()
        {
            InitiaizeTabe();

            while (IsGameRunning())
            {
                _render.RenderTable(_table);

                // Обрабатываем ход каждого живого юнита на поле
                ProcessAllUnitsTurns();

                Console.WriteLine("Нажмите любую клавишу для следующего хода...");
                Console.ReadKey();
            }

            _render.RenderTable(_table);
            EndGame();
        }

        private bool IsGameRunning()
        {
            UpdateCountsAndLeadersFromBoard();

            if (raceACount <= 0 || raceBCount <= 0) return false;
            if (!leaderAAlive || !leaderBAlive) return false;

            return true;
        }

        private void UpdateCountsAndLeaders(List<BaseUnit> raceA, List<BaseUnit> raceB)
        {
            raceACount = raceA.Count(u => u.health > 0);
            raceBCount = raceB.Count(u => u.health > 0);

            var leaderA = raceA.FirstOrDefault(u => u.isLeader);
            var leaderB = raceB.FirstOrDefault(u => u.isLeader);

            leaderAAlive = leaderA == null || leaderA.health > 0;
            leaderBAlive = leaderB == null || leaderB.health > 0;
        }

        private void UpdateCountsAndLeadersFromBoard()
        {
            int countA = 0;
            int countB = 0;
            bool foundLeaderA = false;
            bool foundLeaderB = false;
            bool aliveLeaderA = false;
            bool aliveLeaderB = false;

            for (int x = 0; x < _table.xLen; x++)
            {
                for (int y = 0; y < _table.yLen; y++)
                {
                    var unit = _table._board[x, y]?.placeForUnit;
                    if (unit != null && unit.health > 0)
                    {
                        if (unit.race == Tools.Enums.Race.Human)
                        {
                            countA++;
                            if (unit.isLeader) { foundLeaderA = true; aliveLeaderA = true; }
                        }
                        else if (unit.race == Tools.Enums.Race.Orc)
                        {
                            countB++;
                            if (unit.isLeader) { foundLeaderB = true; aliveLeaderB = true; }
                        }
                    }
                }
            }

            raceACount = countA;
            raceBCount = countB;
            if (foundLeaderA) leaderAAlive = aliveLeaderA;
            if (foundLeaderB) leaderBAlive = aliveLeaderB;
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
            }
        }

        private void EndGame()
        {
            UpdateCountsAndLeadersFromBoard();
            Console.WriteLine("Игра окончена!");
            if (!leaderAAlive) Console.WriteLine("Лидер Людей погиб! Орки победили!");
            else if (!leaderBAlive) Console.WriteLine("Лидер Орков погиб! Люди победили!");
            else if (raceACount <= 0) Console.WriteLine("Все Люди погибли! Орки победили!");
            else if (raceBCount <= 0) Console.WriteLine("Все Орки погибли! Люди победили!");
        }

        private void ProcessMove(int x, int y)
        {
            if (x < 0 || x >= _table.xLen) return;
            if (y < 0 || y >= _table.yLen) return;

            var cell = _table._board[x, y];
            if (cell == null || cell.placeForUnit == null || cell.placeForUnit.health <= 0) return;

            // 1. ИИ пытается найти врага и ударить его, если он в пределах досягаемости
            var targetEnemy = _attack.FindToAttack(cell, _table._board);
            if (targetEnemy != null)
            {
                int damage = 25; // Базовый урон (можно заменить на урон от оружия, если доступно)
                targetEnemy.TakeDamage(damage);
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
