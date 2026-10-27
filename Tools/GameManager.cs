using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Table;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class GameManager : IGameManager
    {
        public event Action<BaseUnit>? OnSomeoneDie;

        private readonly ITable _table;
        private readonly IRender _render;

        private List<BaseUnit> unitsA = new List<BaseUnit>();
        private List<BaseUnit> unitsB = new List<BaseUnit>();

        private bool leaderAAlive = true;
        private bool leaderBAlive = true;

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
            
            string raceAName = unitsA.Count > 0 ? unitsA[0].race.ToString() : "Команда A";
            string raceBName = unitsB.Count > 0 ? unitsB[0].race.ToString() : "Команда B";

            Console.WriteLine($"=== Юнитов {raceAName}: {unitsA.Count} | Юнитов {raceBName}: {unitsB.Count} ===");

            _render.RenderTable(_table, unitsA.Concat(unitsB).ToList(),  unitThatMove);
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

            unitsA = res.raceA;
            unitsB = res.raceB;

            SetBehaviorForTeam(unitsA, req.raceA.isAi);
            SetBehaviorForTeam(unitsB, req.raceB.isAi);

            ShowUnitsBeforeFigth(res.raceA);
            ShowUnitsBeforeFigth(res.raceB);
        }

        private void SetBehaviorForTeam(List<BaseUnit> units, bool isAi)
        {
            AiUnitBehaviour aiBeha = new AiUnitBehaviour(new AiMoveHandler(new Move()), new Attack(new DamageCalculator()), new FindUnit());
            ControlledByHumanUnit humanBeha = new ControlledByHumanUnit(new HumanKeyboardMoveInput(new Move()), new Attack(new DamageCalculator()), new FindUnit());

            IBehaviour beha = isAi ? aiBeha : humanBeha;

            foreach (var unit in units) unit.SetBehaviour(beha);
        }

        private void InterateToMove(int index, List<BaseUnit> teamThatMove) 
        {
            if (teamThatMove.Count < 0) return;

            int currentIndex = index % teamThatMove.Count;
            var unit = teamThatMove[currentIndex];

            if (unit.health < 0) return;

            ProcessMove(unit);
        }

        public void StartGame()
        {
            InitiaizeTabe();

            int indexA = 0;
            int indexB = 0;
            bool isTeamATurn = true;

            while (true)
            {
                if (CheckGameOver(out string winMessage))
                {
                    Render(); 
                    Console.WriteLine($"\n{winMessage}");
                    break;
                }

           
                Render();

                string currentTeamName = isTeamATurn ? "Команда A" : "Команда B";
                Console.WriteLine($"\nНажмите Enter, чтобы сделать ход ({currentTeamName})...");
                Console.ReadLine();

                if (isTeamATurn)
                {
                    InterateToMove(indexA, unitsA);
                    indexA++;
                }
                else
                {
                    InterateToMove(indexB, unitsB);
                    indexB++;
                }

                isTeamATurn = !isTeamATurn;
            }
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
            if (unitsA.Count == 0)
            {
                message = "Все Люди погибли! Орки победили!";
                return true;
            }
            if (unitsB.Count == 0)
            {
                message = "Все Орки погибли! Люди победили!";
                return true;
            }

            message = string.Empty;
            return false;
        }

        private void ProcessMove(BaseUnit unit)
        {
            Render(new Vector2Int(unit.x, unit.y));
            Console.WriteLine($"\nХодит юнит: {unit.race} [ID: {unit.id}] на позиции ({unit.x}, {unit.y})");
            var units = unitsA.Concat(unitsB).ToList();

            unit.MakeMove(new CombatContext
            {
                framesOfTable = _table.GetFrames(),
                allUnits = units,
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

            unitsA.Remove(deadUnit);
            unitsB.Remove(deadUnit);

            if (deadUnit.isLeader)
            {
                if (deadUnit.race == Race.Human) // или проверка по вашей бизнес-логике лидера
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
