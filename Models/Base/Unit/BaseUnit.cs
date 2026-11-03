using PaterniLab2.Interfaces;
using PaterniLab2.Models.States.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Unit
{
    internal abstract class BaseUnit:IClone<BaseUnit>
    {
        public abstract Race race { get; }
        public bool isLeader { get; set; } = false;

        public IUnitState _state { get; private set; }

        public int id { get; private set; }

        public int x { get; private set; }
        public int y { get; private set; }

        public Vector2Int Position => new Vector2Int(x, y);

        protected UnitStats stats = new UnitStats();

        public MoveType moveType { get; protected set; } 

        public IInventory inventory { get; private set; }
        public ISkillContainer skillContainer { get; private set; } //under constraction

        internal IBehaviour behaviour;

        public BaseWeapon weapon { get; protected set; }

        public WeaponType weaponType => weapon.weaponType;
        public int health => stats.health;
        public int armor => stats.armor;
        public int avoidChance => stats.avoidChance;
        public int amountOfMoves => stats.amountOfMoves;
        public int amountOfBattleMoves => stats.amountOfBattleMoves;
        public int damageMultiplayer => stats.damageMultiplayer;
        public int damage => weapon.damage + damageMultiplayer; 

        public abstract string color { get;  }


        private List<StatusEffectObj> statusEffects = new List<StatusEffectObj>();

     
        public BaseUnit(int id, MoveType moveType, BaseWeapon weapons, IInventory inventory)
        {
            this.id = id;
            this.moveType = moveType;
            this.weapon = weapons;
            this.inventory = inventory;

            SetState(new ActiveState());
          
        }

        public void SetBehaviour(IBehaviour behaviour) 
        {
            this.behaviour = behaviour;
        }
        public void SetState(IUnitState state)
        {
            _state?.OnExit(this);
            _state = state;
            _state?.OnEnter(this);
        }

        public void RenewState() => SetState(new ActiveState());

        public bool MakeMove(CombatContext context) 
        {
            if (behaviour == null) throw new Exception("Behaviour is null!!");
            
            return _state.MakeMove(context, this);
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
                case BuffType.Health: stats.ModifyHealth(value);  break;
                case BuffType.Armor:stats.ModifyArmor(value); break;
                case BuffType.AvoidChance: stats.ModifyAvoidChance(value); break;
                case BuffType.Damage: stats.ModifyMultiplayerToDamage(value); break;
            }
        }

        public void ApplyStatusEffect(StatusEffect statusEffect, int value, bool isPositive) 
        {
            int certify = isPositive ? 1 : -1;
            switch (statusEffect) 
            {
                case StatusEffect.Health:stats.ModifyHealth(value * certify);  break;
                case StatusEffect.Armor:stats.ModifyArmor(value * certify);break;
                case StatusEffect.AvoidChance:stats.ModifyAvoidChance(value * certify);  break;
                case StatusEffect.Attack: stats.ModifyMultiplayerToDamage(value * certify); break;
            }
        }


        public void RecieveStatusEffect(RequestOnRecievingStatusEffect newStatusEffect) 
        {
            if (newStatusEffect == null) return;

            var copyOfStatusEffect = statusEffects.FirstOrDefault(sE => sE.statusEffect == newStatusEffect.statusEffects);

            if (copyOfStatusEffect != null)
            {
                copyOfStatusEffect.AddTurns(newStatusEffect.duration);
                return;
            }

            if (newStatusEffect.isOneTimer)
            {
                ApplyStatusEffect(newStatusEffect.statusEffects, newStatusEffect.value, newStatusEffect.isFrendly);
                return;
            }

            var newStatusEffectObj = new StatusEffectObj(newStatusEffect.isFrendly,newStatusEffect.isStacking, newStatusEffect.statusEffects, newStatusEffect.duration, newStatusEffect.value);

            ApplyStatusEffect(newStatusEffect.statusEffects, newStatusEffect.value, newStatusEffect.isFrendly);

            statusEffects.Add(newStatusEffectObj);
        }

        public void EndStatusEffect(StatusEffectObj statusEffectToDelete) 
        {
            if (statusEffectToDelete == null) return;

            if (statusEffectToDelete.statusEffect != StatusEffect.Health) {
                ApplyStatusEffect(statusEffectToDelete.statusEffect, statusEffectToDelete.value, !statusEffectToDelete.isFrendly);
            } 

            statusEffects.Remove(statusEffectToDelete);

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

        public void PutOnTheHorse()
        {
            if (moveType == MoveType.Walk)
            {
                moveType = MoveType.Cavalry;
                stats.ModifyMoves(2);
            }
        }
        public void ElectAsLeader()
        {
            isLeader = true;
        }

        public void TakeDamage(int amount) 
        {
            stats.ModifyHealth(-amount);
            Console.WriteLine($"{race} took {amount} damage. Remaining health: {health}");
        }

        internal void Tick()
        {
            foreach (var statusEffect in statusEffects) 
            {
                if (statusEffect.turnsRamains <= 0) EndStatusEffect(statusEffect);

                if(statusEffect.isStacking) ApplyStatusEffect(statusEffect.statusEffect, statusEffect.value, statusEffect.isFrendly);

                statusEffect.DecreamentTunsRamain();
            }
        }

        public override string ToString()
        {
            string equippedStr = string.Join(", ", inventory.GetAllEquippedItems().Select(item => item));

            return $"Id:{id}, Race: {race}, Health: {health}, Avoid Chance: {avoidChance}, Armor: {armor}, Move Type: {moveType}, Weapon: [{weapon}]\nX: {x}, Y: {y}" +
                $"\n Items:{equippedStr}";
        }


    }
}
