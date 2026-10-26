using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal static class Enums
    {
        public enum Race
        {
            Human,
            Elf,
            Orc,
            Dwarf
        }

        public enum MoveType 
        { 
        Walk,
        Cavalry,
        Fly
        }

        public enum WeaponType 
        { 
        Melee,
        Range
        }
        public enum BuffType
        {
            Health,
            Armor,
            AvoidChance
        }
        public enum ItemType
        {
            Helmet,
            Chestplate,
            Leggings,
            Boots,
            Jewery
            
        }

        public enum StatusEffect
        { 
        Health,
        Armor,
        AvoidChance,
        Attack
        }
        public enum StrategysToFind 
        { 
        Cross,
        Circle
        }


    }
}
