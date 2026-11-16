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
    //Orcs will have this buffs:large additional health,  
    internal class OrcUnit:BaseUnit
    {
        public override string color => "red";
        private const int HEALTH_BUFF = 80;

        public override Race race => Race.Orc;
        public OrcUnit(int id, MoveType moveType, BaseWeapon weapon, IInventory inventory, ISkillContainer skillContainer, BaseLeaderGenerator baseLeaderGenerator)
            : base(id, moveType, weapon, inventory, skillContainer, baseLeaderGenerator)
        {
            stats.ModifyHealth(HEALTH_BUFF);
            ReconsiderInventory();
        }
    }
}
