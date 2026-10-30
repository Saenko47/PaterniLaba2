using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
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
        public OrcUnit(int id, MoveType moveType, BaseWeapon weapon, IInventory inventory)
            : base(id, moveType, weapon, inventory)
        {
            stats.ModifyHealth(HEALTH_BUFF);
            ReconsiderInventory();
        }
    }
}
