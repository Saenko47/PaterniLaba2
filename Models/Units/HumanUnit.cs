using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Items.Humans;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    //Human will have this buffs:  additional health,  additional avoid chance 
    internal class HumanUnit: BaseUnit
    {
        private const int HEALTH_BUFF = 35;
        private const int AVOID_CHANCE = 5;
        public override Race race  => Race.Human;

        public HumanUnit(int id, MoveType moveType, BaseWeapon weapon, IInventory inventory)
            : base(id, moveType, weapon, inventory)
        {
            stats.ModifyHealth(HEALTH_BUFF);
            stats.ModifyAvoidChance(AVOID_CHANCE);
            base.inventory.SetBoot(new HumanBoots());
            ReconsiderInventory();
        }

        
    }
}
