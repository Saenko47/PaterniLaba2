using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Table
{
    internal class PlaceOnBoardRequest
    {
        public Race race { get; set; }

        public int raceMeleeAmount { get; set; }
        public int raceRangeAmount { get; set; }
        public int raceAmountRide { get; set; }
        public int raceAmountFly { get; set; }
    }
}
