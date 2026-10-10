using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    //Orcs will have this buffs:large additional health,  
    internal class OrcUnit:BaseUnit
    {
        private const int HEALTH_BUFF = 80;

        public override Race race => Race.Orc;
        public OrcUnit(int armor, MoveType moveType, BaseWeapon weapon)
            : base(armor, moveType, weapon)
        {
            this.health += HEALTH_BUFF;
        }
    }
}
