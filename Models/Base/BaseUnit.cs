using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base
{
    internal abstract class BaseUnit:IClone<BaseUnit>
    {
        public abstract Race race { get; }

        public int health { get; protected set; }
        public int avaidChance { get; protected set; }
        public int armor { get; protected set; }
        public MoveType moveType { get; protected set; }

        
        public List<BaseWeapon> weapons { get; protected set; } = new List<BaseWeapon>();

        public BaseUnit(int health, int avaidChance, int armor, MoveType moveType, List<BaseWeapon> weapons)
        {
            this.health = health;
            this.avaidChance = avaidChance;
            this.armor = armor;
            this.moveType = moveType;
            this.weapons = weapons;
        }

        public virtual BaseUnit Clone()
        {
           
            BaseUnit clone = (BaseUnit)this.MemberwiseClone();

          
            clone.weapons = this.weapons.Select(w => w.Clone()).ToList();

            return clone;
        }


    }
}
