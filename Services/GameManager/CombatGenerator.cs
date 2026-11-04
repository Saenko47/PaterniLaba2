using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.GameManager
{
    internal class CombatGenerator:ICombatGenerator
    {
        private readonly IRender _render;
        private readonly ITable _table;
        private List<Army> _armies;

        public event Action<BaseUnit>? OnSomeoneDie;

        public CombatGenerator(IRender render,ITable table) 
        {
            _render = render;
            
            _table = table;

        }

        public void InitializeArmies(List<Army> armies) 
        {
            _armies = armies;
            foreach (var army in armies)
            {
                army.OnUnitHit += HandleArmyHighlightRequest;
            }

        }

        private void HandleArmyHighlightRequest(BaseUnit request)
        {
            var req = new RequestOnHighlihtUnit { position = request.Position, highlightType = HighlightType.AttacedUnit };
            _render.Render(_armies, new List<RequestOnHighlihtUnit>{ req });
        }

        private bool CheckGameOver(out string message)
        {
            var leadersAlive = _armies.Where(a => a._isLeaderAlive).ToList();

            if (leadersAlive.Count <= 1)
            {
                message = leadersAlive.Count == 1
                    ? $"{leadersAlive[0]._race} is WIN!!"
                    : "Draw / No leaders left!";
                return true;
            }
            var armiesWithUnits = _armies.Where(a => a.HasUnits).ToList();

            if (armiesWithUnits.Count <= 1)
            {
                message = leadersAlive.Count == 1
                   ? $"{armiesWithUnits[0]._race} is WIN!!"
                   : "Draw / No units left!";
                return true;
            }

            message = string.Empty;
            return false;
        }

        public void GenerateCombat()
        {
            if (_armies == null || _armies.Count < 0) return;

            int armyIndex = 0;

            while (true)
            {
                if (CheckGameOver(out string winMessage))
                {
                    _render.Render(_armies);
                    Console.WriteLine($"\n{winMessage}");
                    return;
                }

                while (!CheckIfAllInArmiesMakeMove())
                {


                    int indexOfCurrentArmy = armyIndex % _armies.Count;
                    var currentArmyToMove = _armies[indexOfCurrentArmy];
                    if (currentArmyToMove.IsAllMakeMoves())
                    {
                        armyIndex++;
                        continue;
                    }
                    Console.WriteLine($"\nНажмите Enter, чтобы сделать ход");
                    Console.ReadLine();


                    bool isLock = InterateToMove(currentArmyToMove);

                    Console.WriteLine(isLock);
                    if (!isLock) break;
                    armyIndex++;

                }

                foreach (var army in _armies) army.PrepareForNewRound();
            }
        }

        private void Render(List<RequestOnHighlihtUnit>? highlightRequests = null)
        {

            List<(string name, int count)> unitsNameAndCounts = _armies.Select(a => (name: a._race.ToString(), count: a._units.Count)).ToList();

            foreach (var army in unitsNameAndCounts) Console.WriteLine($"Name:{army.name}, amount: {army.count}");

            _render.Render(_armies, highlightRequests);
        }

        private bool ProcessMove(BaseUnit unit)
        {
            Console.Clear();
            _render.Render(_armies,new List<RequestOnHighlihtUnit>
                {
                    new RequestOnHighlihtUnit
                    {
                        position = unit.Position,
                        highlightType = HighlightType.ActiveUnit
                    }
                });

            Console.WriteLine($"\nХодит юнит: {unit.race} [ID: {unit.id}] на позиции ({unit.x}, {unit.y})");

            return unit.MakeMove(new TableContext
            {
                framesOfTable = _table.GetFrames(),
                armies = _armies,
                isCellFree = _table.IsSellFree,
                onUnitDeath = OnSomeoneDie,
                onMove = _table.MoveUnit,

                updateRender = Render
            });
        }
        private bool InterateToMove(Army teamThatMove)
        {

            var unit = teamThatMove.GetUnitToMove();

            if (unit == null)
            {
                return false;
            }
            ProcessMove(unit);
            return true;
        }

        private bool CheckIfAllInArmiesMakeMove()
        {
            return _armies.All(a => a.IsAllMakeMoves());
        }
        
    }

}
    
