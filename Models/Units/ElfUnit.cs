using PaterniLab2.Models.Base;
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
        public ElfUnit(int health, int avaidChance, int armor, MoveType moveType, List<BaseWeapon> weapons)
            : base(health, avaidChance, armor, moveType, weapons)
        {
            this.avaidChance += AVOID_CHANCE;
        }

      
    }
}
