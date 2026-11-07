using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Services.LeaderGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    //Elves will have this buffs: large additional avoid chance,  
    internal class ElfUnit:BaseUnit
    {
        public override string color => "green";
        private const int AVOID_CHANCE = 20;
        public override Race race => Race.Elf;
        public ElfUnit(int id, MoveType moveType, BaseWeapon weapon, IInventory inventory, BaseLeaderGenerator baseLeaderGenerator)
            : base(id, moveType, weapon, inventory, baseLeaderGenerator)
        {
            stats.ModifyAvoidChance(AVOID_CHANCE);
            ReconsiderInventory();
        }

      
    }
}
