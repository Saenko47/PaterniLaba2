using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Placement
{
    internal class AutomaticPlace2Units: BasePlacementOfUnits
    {
        public AutomaticPlace2Units(ICreateUnitByRace createUnitByRace) : base(createUnitByRace) { }
        

        private BaseUnit? AutomaticChooseUnitToPlace(PlaceOnBoardRequest req)
        {
            BaseUnit? unitToPlace = null;
            if (req.raceMeleeAmount > 0)
            {
                unitToPlace =  ChooseUnitToPlace(UnitTypeToPlace.Melee, req);
            }
            else if (req.raceRangeAmount > 0)
            {
                unitToPlace = ChooseUnitToPlace(UnitTypeToPlace.Range, req);
            }
            else
            {
                throw new InvalidOperationException("No units available to place.");
            }
            return unitToPlace;
        }

        public override Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req, Cell[,] table)
        {

            if (req == null || table == null) return new Army();

            int xLim = table.GetLength(0);
            int yLim = table.GetLength(1);

            if (xLim == 0 || yLim == 0) return new Army();

            int y = (table[0, 0].isEmpty == true) ? 0 : yLim - 1;
            int stepY = (y == 0) ? 1 : -1;

            List<BaseUnit> placedUnits = new List<BaseUnit>();

            while (y >= 0 && y < yLim && req.isRamainToPlace)
            {
                for (int x = 0; x < xLim && req.isRamainToPlace; x++)
                {
                    if (table[x, y].isEmpty == true)
                    {
                        var unitToPlace = AutomaticChooseUnitToPlace(req);

                        if (unitToPlace == null) break;

                        if (placedUnits.Count == 0)
                        {
                            ChooseLeader(unitToPlace);
                        }

                        unitToPlace.SetPosition(x, y);
                        table[x, y].isEmpty = false;
                        placedUnits.Add(unitToPlace);

                       
                    }
                }

                y += stepY;
            }

            return new Army(req.race, placedUnits, req.isAi);
        }
    }
}
