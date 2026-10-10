using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Table
{
    internal class CreateGameRequest
    {
        public PlaceOnBoardRequest raceA { get; set; } = null!;
        public PlaceOnBoardRequest raceB { get; set; } = null!;
    }
}
