using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Placement;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Placement
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
            switch (decision)
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
            

            Console.WriteLine("Choose unit type to place:");

            // Динамически показываем доступные варианты в зависимости от остатка в req
            if (req.raceMeleeAmount > 0) Console.WriteLine("1 - Melee");
            if (req.raceRangeAmount > 0) Console.WriteLine("2 - Range");
            if (req.raceMeleeAmount > 0 && req.raceAmountRide > 0) Console.WriteLine("3 - Cavalry Melee");
            if (req.raceRangeAmount > 0 && req.raceAmountRide > 0) Console.WriteLine("4 - Cavalry Range");
            Console.WriteLine("5 - Undo");

            if (!int.TryParse(Console.ReadLine(), out int TypeOfUnitToPlace))
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

            // Дополнительная защита: проверяем, действительно ли можно выбрать этот тип юнита
            bool canPlace = TypeOfUnitToPlace switch
            {
                1 => req.raceMeleeAmount > 0,
                2 => req.raceRangeAmount > 0,
                3 => req.raceMeleeAmount > 0 && req.raceAmountRide > 0,
                4 => req.raceRangeAmount > 0 && req.raceAmountRide > 0,
                _ => false
            };

            if (!canPlace)
            {
                Console.WriteLine("You don't have enough units of this type (or ride units) to place!");
                return null;
            }

            Console.WriteLine("Enter the coordinates to place the unit (x y):");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) return null;

            string[] coordinates = input.Split(' ');
            if (coordinates.Length < 2)
            {
                Console.WriteLine("Invalid coordinates format.");
                return null;
            }

            int x = Convert.ToInt32(coordinates[0]);
            int y = Convert.ToInt32(coordinates[1]);

            // Проверка границ и занятости клетки
            if (x >= 0 && x < table.GetLength(0) && y >= 0 && y < table.GetLength(1))
            {
                if (!table[x, y].isEmpty)
                {
                    Console.WriteLine("This cell is already occupied!");
                    return null;
                }

                // Проверка стороны игрока
                bool isLeftSideOrTop = WhatSidePlayerHave(table);
                bool isAllowedSide = isLeftSideOrTop ? (x < frames.X) : (x >= frames.X);

                if (!isAllowedSide)
                {
                    Console.WriteLine("You cannot place units on the opponent's side of the board!");
                    return null;
                }

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
            else
            {
                Console.WriteLine("Coordinates are out of bounds!");
            }

            return null;
        }

        private bool WhatSidePlayerHave(Cell[,] table)
        {
            return table[0, 0].isEmpty; 
        }

        public override Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req, Cell[,] table)
        {
            int xLim = table.GetLength(0) / 2;
            int yLim = table.GetLength(1) / 2;
            Vector2Int frames = new Vector2Int(xLim, yLim);

            List<BaseUnit> placedUnits = new List<BaseUnit>();

            while (req.isRamainToPlace)
            {
                var unit = ChooseToPlace(req, frames, table, placedUnits);
                if (unit != null)
                {
                    placedUnits.Add(unit);
                    table[unit.Position.X, unit.Position.Y].isEmpty = false;
                    if (!req.isLeaderSet)
                    {
                        ChooseLeader(unit);
                    }
                }
                
            }

            return new Army(req.race, placedUnits, req.isAi);
        }
    }
}
