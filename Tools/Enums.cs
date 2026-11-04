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
            AvoidChance,
            Damage
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

        public enum UnitStateEnum
        { 
        Active,
        Unactive,
        Delayed,
        Stuned
        }

        public enum UnitTypeToPlace 
        { 
        Melee,
        CavalryMelee,
        Range,
        CavalryRange,

        }

        public enum HighlightType
        {
            ActiveUnit,
            TargetUnit,
            AttackableUnit,
            MoveableCell,
            AttackableCell,
            AttacedUnit,
        }

        public enum ResultOfAction
        {
            SuccessMove,
            SuccessAction,
            FailToMove,
            FailToAttack,
            Awaiting
        }

    }
}
