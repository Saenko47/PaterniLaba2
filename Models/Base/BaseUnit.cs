using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base
{
    internal abstract class BaseUnit:IClone<BaseUnit>
    {

        public int x { get; private set; }
        public int y { get; private set; }
        public abstract Race race { get; }
       

        public int health { get; protected set; } = 100;
        public int avaidChance { get; protected set; } = 5;
        public int armor { get; protected set; }
        public MoveType moveType { get; protected set; }

        public bool isLeader { get; set; } = false;


        public BaseWeapon weapon { get; protected set; }
        public WeaponType weaponType => weapon.weaponType;

        public List<IItem> inventory { get; } = new List<IItem>();
        

        public BaseUnit(int armor, MoveType moveType, BaseWeapon weapons)
        {
           
            this.armor = armor;
            this.moveType = moveType;
            this.weapon = weapons;
        }
        public void AddItem(IItem item)
        {
            if (item == null) return;

            inventory.Add(item);
            ApplyBuffs(item);
        }

      
        public void RemoveItem(IItem item)
        {
            if (item == null || !inventory.Contains(item)) return;

            inventory.Remove(item);
            RemoveBuffs(item); 
        }
        private void ApplyBuffs(IItem item)
        {
            foreach (var buff in item.Buffs)
            {
                switch (buff.Key)
                {
                    case ItemBuffType.Health:
                        this.health += buff.Value;
                        break;
                    case ItemBuffType.Armor:
                        this.armor += buff.Value;
                        break;
                    case ItemBuffType.AvoidChance:
                        this.avaidChance += buff.Value;
                        break;
                }
            }
        }
        private void RemoveBuffs(IItem item)
        {
            foreach (var buff in item.Buffs)
            {
                switch (buff.Key)
                {
                    case ItemBuffType.Health:
                        this.health -= buff.Value; 
                        break;
                    case ItemBuffType.Armor:
                        this.armor -= buff.Value;
                        break;
                    case ItemBuffType.AvoidChance:
                        this.avaidChance -= buff.Value;
                        break;
                }
            }
        }
        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
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
            return $"Race: {race}, Health: {health}, Avoid Chance: {avaidChance}, Armor: {armor}, Move Type: {moveType}, Weapon: [{weapon}]\nX: {x}, Y: {y}";
        }


    }
}
