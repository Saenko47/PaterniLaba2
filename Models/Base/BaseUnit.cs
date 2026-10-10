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
       

        public int health { get; protected set; } = 100;
        public int avaidChance { get; protected set; } = 5;
        public int armor { get; protected set; }
        public MoveType moveType { get; protected set; }

        public bool isLeader { get; set; } = false;


        public BaseWeapon weapon { get; protected set; }
        public WeaponType weaponType => weapon.weaponType;
        

        public BaseUnit(int armor, MoveType moveType, BaseWeapon weapons)
        {
           
            this.armor = armor;
            this.moveType = moveType;
            this.weapon = weapons;
        }

        public virtual BaseUnit Clone()
        {
           
            BaseUnit clone = (BaseUnit)this.MemberwiseClone();


            clone.weapon = weapon.Clone();

            return clone;
        }

        public void GiveWeaponToUnit(BaseWeapon newWeapon) 
        {
            if (weapon != newWeapon)
            {
                weapon = newWeapon;
            }
        } 
        public void ElectAsLeader()
        {
            isLeader = true;
        }

        public void TakeDamage(int amount) 
        {
            this.health -= amount;
            Console.WriteLine($"{race} took {amount} damage. Remaining health: {health}");
        }

        public override string ToString()
        {
            return $"Race: {race}, Health: {health}, Avoid Chance: {avaidChance}, Armor: {armor}, Move Type: {moveType}, Weapon: [{weapon}]";
        }


    }
}
