using PaterniLab2.Models.Base;
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

        public HumanUnit(int health, int avaidChance, int armor, MoveType moveType, List<BaseWeapon> weapons)
        : base(health, avaidChance, armor, moveType, weapons)
        {
            health += HEALTH_BUFF;
            avaidChance += AVOID_CHANCE;
        }

        
    }
}
