using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IPlacementManager
    {
        Army Place(PlaceOnBoardRequest req, bool isAutmatic, Cell[,] cells);
        void SetMode(bool mode);
    }
}
