using Microsoft.VisualBasic;
using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Table;
using PaterniLab2.Services.AttackAndDamageProccesing;
using PaterniLab2.Services.Behaviour;
using PaterniLab2.Services.MoveHandlers;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class GameManager : IGameManager
    {
        public event Action<BaseUnit>? OnSomeoneDie;

        private readonly ITable _table;
        private readonly IRender _render;

        private List<Army> armies = new List<Army>();

        public GameManager(ITable table, IRender render)
        {
            _table = table;
            _render = render;
            OnSomeoneDie += HandleUnitDeath;
        }

        private void ShowUnitsBeforeFigth(List<BaseUnit> units)
        {
            foreach (var unit in units)
            {
                Console.WriteLine(unit);
            }
        }

        private void Render(Vector2Int? unitThatMove = null)
        {

            List<(string name, int count)> unitsNameAndCounts = armies.Select(a => (name: a._race.ToString(), count: a._units.Count)).ToList();

            foreach (var army in unitsNameAndCounts) Console.WriteLine($"Name:{army.name}, amount: {army.count}");

            var allUnits = armies[0]._units.Concat(armies[1]._units).ToList();

            _render.RenderTable(_table, allUnits,  unitThatMove);
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
                    raceRangeAmount = 1,
                    isAi = false
                },
                raceB = new Requests.Table.PlaceOnBoardRequest
                {
                    race = Race.Orc,
                    raceAmountFly = 0,
                    raceAmountRide = 0,
                    raceMeleeAmount = 0,
                    raceRangeAmount = 2,
                    isAi = true
                }
            };

            var res = _table.FirstPlacementOfUnits(req);

            armies.Add(new Army(res.raceA[0].race, res.raceA, false));
            armies.Add(new Army(res.raceB[0].race, res.raceB, true));


            foreach (var army in armies) SetBehaviorForTeam(army._units, army._isAi);

            foreach (var army in armies) ShowUnitsBeforeFigth(army._units);

              
        }

        private void SetBehaviorForTeam(List<BaseUnit> units, bool isAi)
        {
            AiUnitBehaviour aiBeha = new AiUnitBehaviour(new AiMoveHandler(new Move()), new Attack(new DamageCalculator()), new FindUnit());
            ControlledByHumanUnit humanBeha = new ControlledByHumanUnit(new HumanKeyboardMoveInput(new Move()), new Attack(new DamageCalculator()), new FindUnit());

            IBehaviour beha = isAi ? aiBeha : humanBeha;

            foreach (var unit in units) unit.SetBehaviour(beha);
        }

        private void InterateToMove(Army teamThatMove) 
        {
            if (teamThatMove._units.Count < 0) return;

            int currentIndex = teamThatMove.currentIndex % teamThatMove._units.Count;
            var unit = teamThatMove._units[currentIndex];

            teamThatMove.IncrementIndex();

            if (unit.health < 0) return;

            ProcessMove(unit);
        }

        public void StartGame()
        {
            InitiaizeTabe();

            int armyIndex = 0;

            while (true)
            {
                if (CheckGameOver(out string winMessage))
                {
                    Render();
                    Console.WriteLine($"\n{winMessage}");
                    return;
                }


                Render();

                int indexOfCurrentArmy = armyIndex % armies.Count;
                var currentArmyToMove = armies[indexOfCurrentArmy];
                Console.WriteLine($"\nНажмите Enter, чтобы сделать ход");
                Console.ReadLine();

             
                InterateToMove(currentArmyToMove);
                armyIndex++;

            }

            
        }

        private bool CheckGameOver(out string message)
        {
            var leadersAlive = armies.Where(a => a._isLeaderAlive).ToList();

            if (leadersAlive.Count <= 1) 
            {
                message = leadersAlive.Count == 1
                    ? $"{leadersAlive[0]._race} is WIN!!"
                    : "Draw / No leaders left!";
                return true; 
            }
            var armiesWithUnits = armies.Where(a => a.HasUnits).ToList();

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

        private void ProcessMove(BaseUnit unit)
        {
            Render(new Vector2Int(unit.x, unit.y));
            Console.WriteLine($"\nХодит юнит: {unit.race} [ID: {unit.id}] на позиции ({unit.x}, {unit.y})");
            var allUnits = armies[0]._units.Concat(armies[1]._units).ToList();

            unit.MakeMove(new CombatContext
            {
                framesOfTable = _table.GetFrames(),
                allUnits = allUnits,
                isCellFree = _table.IsSellFree,
                onUnitDeath = OnSomeoneDie,
                onMove = _table.MoveUnit,
               
                updateRender = Render
            });
        }

        private void HandleUnitDeath(BaseUnit deadUnit)
        {
            if (deadUnit == null) return;

            _table.ClearCell(deadUnit.x, deadUnit.y);

            var armyWherHeServe = armies.FirstOrDefault(a => a._race == deadUnit.race);
            if (armyWherHeServe == null) return;

            armyWherHeServe.RemoveUnit(deadUnit);

            if (deadUnit.isLeader)
            {
                armyWherHeServe.KillLeader();
            }

            Console.WriteLine($"{deadUnit.race} unit has died.");
        }

        public void InvokeUnitDeath(BaseUnit deadUnit)
        {
            OnSomeoneDie?.Invoke(deadUnit);
        }
    }
}
