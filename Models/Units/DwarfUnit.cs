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
    //Dwarfs will have this buffs:large additional armor, additional health
    internal class DwarfUnit:BaseUnit
    {
        public override string color => "magenta";
        private const int ARMOR_BUFF = 20;
        private const int HEALTH_BUFF = 20;
        public override Race race => Race.Dwarf;
        public DwarfUnit(int id, MoveType moveType, BaseWeapon weapon, IInventory inventory,ISkillContainer skillContainer, BaseLeaderGenerator baseLeaderGenerator)
            : base(id, moveType, weapon, inventory, skillContainer, baseLeaderGenerator)
        {
            stats.ModifyArmor(ARMOR_BUFF);
            stats.ModifyHealth(HEALTH_BUFF);
            ReconsiderInventory();
        }

      
    }
}
