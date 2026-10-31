using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
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

        public Army Place(PlaceOnBoardRequest req, bool isAutmatic, Cell[,] cells) 
        {
            var placementMode = isAutmatic ? _automaticPlacement : _handlyPlacement;

            return placementMode.Place(req, isTwo, cells);
        }
    }
}
