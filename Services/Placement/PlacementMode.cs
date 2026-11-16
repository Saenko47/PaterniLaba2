using PaterniLab2.Interfaces.Placement;
using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.Placement
{
    internal class PlacementMode:IPlacementMode
    {
        private BasePlacementOfUnits _forTwo;
        private BasePlacementOfUnits _forMoreThanTwo;
        public PlacementMode(BasePlacementOfUnits forTwo, BasePlacementOfUnits forMoreThanTwo) 
        {
            _forTwo = forTwo;
            _forMoreThanTwo = forMoreThanTwo;
        }

        public Army Place(PlaceOnBoardRequest req, bool isTwo, Cell[,] cells) 
        {
            Army newArmy = isTwo ? _forTwo.PaceUnitsOnTheBoard(req, cells) : _forMoreThanTwo.PaceUnitsOnTheBoard(req, cells);
            return newArmy;
        }

    }
}
