using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IPlaceRaceOnBoard
    {
        List<BaseUnit> PlaceOnBoard(PlaceOnBoardRequest req, Cell[,] table);
    }
}
