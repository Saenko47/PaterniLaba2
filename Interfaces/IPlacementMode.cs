using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IPlacementMode
    {
        public Army Place(PlaceOnBoardRequest req, bool isTwo, Cell[,] cells);
    }
}
