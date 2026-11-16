using PaterniLab2.Interfaces.Placement;
using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.Placement
{
    internal class PlacementManager:IPlacementManager
    {
        private readonly IPlacementMode _automaticPlacement;
        private readonly IPlacementMode _handlyPlacement;

        private bool isTwo;

        public PlacementManager(IPlacementMode automatic, IPlacementMode handly) 
        {
            _automaticPlacement = automatic;
            _handlyPlacement = handly;
         
          
        }

        public void SetMode(bool mode) => isTwo = mode;

        public Army Place(PlaceOnBoardRequest req, bool isHandlyPlacement, Cell[,] cells) 
        {
            var placementMode = isHandlyPlacement ? _handlyPlacement : _automaticPlacement;

            return placementMode.Place(req, isTwo, cells);
        }
    }
}
