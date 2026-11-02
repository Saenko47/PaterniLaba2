using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class HandlyPlace2Units : BasePlacementOfUnits
    {
        private Stack<IPlaceCommand> _commands = new Stack<IPlaceCommand>();
        public HandlyPlace2Units(ICreateUnitByRace createUnitByRace) : base(createUnitByRace) { }


        private void ShowWhoCanBePlaced(PlaceOnBoardRequest req)
        {
            Console.WriteLine($"You can place {req.raceMeleeAmount} melee units, {req.raceRangeAmount} range units, and {req.raceAmountRide} ride units of race {req.race}");
        }

        private BaseUnit? ChooseUnitTypeToPlace(PlaceOnBoardRequest req, int decision)
        {
            switch(decision)
            {
                case 1:
                    return ChooseUnitToPlace(UnitTypeToPlace.Melee, req);
                   
                case 2:
                    return ChooseUnitToPlace(UnitTypeToPlace.Range, req);
                    
                case 3:
                    return ChooseUnitToPlace(UnitTypeToPlace.CavalryMelee, req);
                case 4:
                    return ChooseUnitToPlace(UnitTypeToPlace.CavalryRange, req);
            }
            return null;
        }

        private BaseUnit? ChooseToPlace(PlaceOnBoardRequest req, Vector2Int frames, Cell[,] table, List<BaseUnit> placedUnits) 
        {
            ShowWhoCanBePlaced(req);

            Console.WriteLine("Choose unit type to place: 1 - Melee, 2 - Range, 3 - Cavalry Melee, 4 - Cavalry Range 5 - Undo");

            if(!int.TryParse(Console.ReadLine(), out int TypeOfUnitToPlace))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return null;
            }

            if (TypeOfUnitToPlace == 5)
            {
                if (_commands.Count > 0)
                {
                    var lastCommand = _commands.Pop();
                    lastCommand.Undo();
                    Console.WriteLine("Last placement undone.");
                }
                return null;
            }
            Console.WriteLine("Enter the coordinates to place the unit (x y):");
            string[] coordinates = Console.ReadLine().Split(' ');
            int x = Convert.ToInt32(coordinates[0]);
            int y = Convert.ToInt32(coordinates[1]);
            if (x >= 0 && x < frames.X && y >= 0 && y < frames.Y)
            {
                BaseUnit? unit = ChooseUnitTypeToPlace(req, TypeOfUnitToPlace);
                if (unit != null)
                {
                    var placeCommand = new PlaceCommand(unit, new Vector2Int(x, y), table, req, (UnitTypeToPlace)TypeOfUnitToPlace, placedUnits);
                    placeCommand.Execute();
                    _commands.Push(placeCommand);
                    Console.WriteLine($"Placed {unit} at ({x}, {y})");
                    return unit;
                }
            }
            return null;

        }

        public override Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req, Cell[,] table)
        {
            int xLim = table.GetLength(0) / 2;
            int yLim = table.GetLength(1);
            Vector2Int frames = new Vector2Int(xLim, yLim);

            List<BaseUnit> placedUnits = new List<BaseUnit>();

            while (req.isRamainToPlace)
            {
                
                var unit = ChooseToPlace(req, frames, table, placedUnits);
                if (unit != null)
                {
                    placedUnits.Add(unit);
                    if (!req.isLeaderSet) 
                    { 
                    ChooseLeader(unit);
                    }
                }
                req.RenderPlacement?.Invoke();
            }

            return new Army(req.race,placedUnits, req.isAi);
        }
    }
}
