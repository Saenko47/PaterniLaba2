using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IPlaceUnits
    {
        Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req);
    }
}
