using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Unit
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

        public IInventory inventory { get; private set; }
        public ISkillContainer skillContainer { get; private set; }

        private IBehaviour behaviour;

        public BaseWeapon weapon { get; protected set; }
        public WeaponType weaponType => weapon.weaponType;

        public int amountOfMoves => moveType == MoveType.Cavalry ? 2 : 1;
        public int amountOfBattleMoves { get; private set; } = 1;

     


        public BaseUnit(int armor, MoveType moveType, BaseWeapon weapons, IInventory inventory)
        {
           
            this.armor = armor;
            this.moveType = moveType;
            this.weapon = weapons;
            this.inventory = inventory;
          
        }
        public void SetBehaviour(IBehaviour behaviour) 
        {
            this.behaviour = behaviour;
        }

        public void MakeMove(CombatContext context) 
        {
            if (behaviour == null) return;
            behaviour.MakeMove(context);
        }

       

        protected void ReconsiderInventory()
        {
            if (inventory == null) return;

            foreach (var item in inventory.GetAllEquippedItems())
            {
                if (item?.buffs == null) continue;

                foreach (var buff in item.buffs)
                {
                    ApplyBuff(buff.Key, buff.Value);
                }
            }
        }


        public void ApplyBuff(BuffType buffType, int value)
        {
            switch (buffType)
            {
                case BuffType.Health:
                    health += value;
                    break;
                case BuffType.Armor:
                    armor += value;
                    break;
                case BuffType.AvoidChance:
                    avaidChance += value; 
                    break;
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
            string equippedStr = string.Join(", ", inventory.GetAllEquippedItems().Select(item => item));

            return $"Race: {race}, Health: {health}, Avoid Chance: {avaidChance}, Armor: {armor}, Move Type: {moveType}, Weapon: [{weapon}]\nX: {x}, Y: {y}" +
                $"\n Items:{equippedStr}";
        }


        private void Tick()
        {

        }


    }
}
