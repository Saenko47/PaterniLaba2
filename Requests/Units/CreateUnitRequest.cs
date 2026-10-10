using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Units
{
    internal class CreateUnitRequest
    {
       

        public int health { get;  set; }
        public int avaidChance { get;  set; }
        public int armor { get;  set; }
        public MoveType moveType { get;  set; }


        public BaseWeapon weapon { get; set; } = null!;
    }
}
