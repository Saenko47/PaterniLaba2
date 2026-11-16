using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class UnitStringify
    {
        private InventoryDto TranserInventoryToDto(IInventory inventory) 
        {
            InventoryDto inventoryDto = new InventoryDto();
            inventoryDto.JeweryMaxCount = inventory.GetMaxJewery();
            foreach (var item in inventory.GetAllEquippedItems()) 
            {
                inventoryDto.items.Add(new ItemDto(item.Id, item.itemType));
            }
            return inventoryDto;
        }

        private SkillContainerDto TransferSkillConteinerToDto(ISkillContainer skillContainer) 
        {
            SkillContainerDto skillContainerDto = new SkillContainerDto();
            skillContainerDto.MaxSkills = skillContainer.amountOfSkills;

            foreach (var skill in skillContainer.GetSkills()) 
            {
                skillContainerDto.skills.Add(new SkillDto(skill.id, skill.statusEffect));
            }

            return skillContainerDto;
        }
        private UnitStatsDto TransferUnitStatsToDto(UnitStats stats) 
        {
            UnitStatsDto unitStatsDto = new UnitStatsDto
            {
                Health = stats.health,
                AmountOfBattleMoves = stats.amountOfBattleMoves,
                AmountOfMoves = stats.amountOfMoves,
                Armor = stats.armor,
                AvoidChance = stats.avoidChance,
                DamageMultiplayer = stats.damageMultiplayer
            };
            return unitStatsDto;
        }

        private List<StatusEffectObjDto> TransferStatusEffectsToDto(List<StatusEffectObj> statusEffectObjs) 
        {
            var statusEffectObjDtos = new List<StatusEffectObjDto>();
            foreach (var statusEffectObj in statusEffectObjs)
            {
                statusEffectObjDtos.Add(new StatusEffectObjDto
                {
                    IsFrendly = statusEffectObj.isFrendly,
                    IsStacking = statusEffectObj.isStacking,
                    StatusEffect = statusEffectObj.statusEffect,
                    TurnsRemains = statusEffectObj.turnsRamains,
                    Value = statusEffectObj.value
                });
            }
            return statusEffectObjDtos;
        }


        public UnitDto UnitToJson(BaseUnit unitToStringify) 
        {
            var unitDto = new UnitDto();

            unitDto.Id = unitToStringify.id;
            unitDto.IsLeader = unitToStringify.isLeader;
            unitDto.Race = unitToStringify.race;
            unitDto.WeaponType = unitToStringify.weaponType;
            unitDto.MoveType = unitToStringify.moveType;
            unitDto.X = unitToStringify.x;
            unitDto.Y = unitToStringify.y;
            unitDto.state = unitToStringify._state.state;
            unitDto.Inventory = TranserInventoryToDto(unitToStringify.inventory);
            unitDto.SkillContainer = TransferSkillConteinerToDto(unitToStringify.skillContainer);
            unitDto.StatusEffects = TransferStatusEffectsToDto(unitToStringify.statusEffects);
            unitDto.Stats = TransferUnitStatsToDto(unitToStringify.stats);
            unitDto.behaviourType = unitToStringify.behaviour.behaviourType;


            return unitDto;

        }
    }
}
