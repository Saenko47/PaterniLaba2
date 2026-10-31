using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Table
{
    internal class CreateGameRequest
    {
        public PlaceOnBoardRequest[] reqs = new PlaceOnBoardRequest[4];

        public int amountOfRacesOnBoard => reqs.Count(r => r != null);
    }
}
