using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using PaterniLab2.Models.Units;
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
        public bool isAi { get; set; }
        public bool isHandlyPlacement { get; set; }

        

        public int entireAmount => raceMeleeAmount + raceRangeAmount;
        public bool isRamainToPlace => entireAmount > 0;
        public bool isLeaderSet { get; set; } = false;
    }
}
