using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using PaterniLab2.Requests.Table;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class PlaceRaceOnBoard: IPlaceRaceOnBoard
    {
        private readonly ICreateUnitByRace _createUnitByRace;

        public PlaceRaceOnBoard(ICreateUnitByRace createUnitByRace) 
        {
            _createUnitByRace = createUnitByRace;
        }

        private BaseUnit? ChooseUnitToPlace(PlaceOnBoardRequest req)
        {
            if (req.raceMeleeAmount > 0)
            {
                req.raceMeleeAmount--;
                return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Melee, MoveType.Walk);
            }

            if (req.raceRangeAmount > 0)
            {
                req.raceRangeAmount--;
                return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Range, MoveType.Walk);
            }

            return null; // Обязательный дефолтный возврат, если юнитов нет
        }

        public List<BaseUnit> PlaceOnBoard(PlaceOnBoardRequest req, Cell[,] table)
        {
            if (req == null || table == null) return new List<BaseUnit>();

            int xLim = table.GetLength(0);
            int yLim = table.GetLength(1);

            if (xLim == 0 || yLim == 0) return new List<BaseUnit>();

            // Determine starting row and direction
            int y = (table[0, 0].placeForUnit == null) ? 0 : yLim - 1;
            int stepY = (y == 0) ? 1 : -1;

            // Loop until row bounds are exceeded OR units run out
            List<BaseUnit> placedUnits = new List<BaseUnit>();
            while (y >= 0 && y < yLim )
            {
                if (req.raceRangeAmount <= 0 && req.raceMeleeAmount <= 0) break;
                for (int x = 0; x < xLim; x++)
                {
                    // Optional check: stop placing if unit counts hit 0 mid-row
                    if (req.raceRangeAmount <= 0 && req.raceMeleeAmount <= 0) break;

                    var unitToPlace = ChooseUnitToPlace(req);

                    if (table[x, y] != null && unitToPlace != null)
                    {
                        table[x, y].placeForUnit = unitToPlace;
                        placedUnits.Add(unitToPlace);
                    }
                }

                y += stepY;
            }
            return placedUnits;
        }
    }
}
