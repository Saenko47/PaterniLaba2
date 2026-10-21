using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    //Elves will have this buffs: large additional avoid chance,  
    internal class ElfUnit:BaseUnit
    {
        private const int AVOID_CHANCE = 20;
        public override Race race => Race.Elf;
        public ElfUnit(int armor, MoveType moveType, BaseWeapon weapon, IInventory inventory)
            : base(armor, moveType, weapon, inventory)
        {
            this.avaidChance += AVOID_CHANCE;
            ReconsiderInventory();
        }

      
    }
}
