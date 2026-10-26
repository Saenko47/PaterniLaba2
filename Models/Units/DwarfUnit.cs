using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    //Dwarfs will have this buffs:large additional armor, additional health
    internal class DwarfUnit:BaseUnit
    {
        private const int ARMOR_BUFF = 20;
        private const int HEALTH_BUFF = 20;
        public override Race race => Race.Dwarf;
        public DwarfUnit(int id, int armor, MoveType moveType, BaseWeapon weapon, IInventory inventory)
            : base(id, armor, moveType, weapon, inventory)
        {
            this.armor += ARMOR_BUFF;
            this.health += HEALTH_BUFF;
            ReconsiderInventory();
        }

      
    }
}
