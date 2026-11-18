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
        private InventoryDto TransferInventoryToDto(IInventory inventory) =>
            new InventoryDto(
                Items: inventory.GetAllEquippedItems()
                    .Select(item => new ItemDto(item.Id, item.itemType))
                    .ToList(),
                JewelryMaxCount: inventory.GetMaxJewery()
            );

        private SkillContainerDto TransferSkillContainerToDto(ISkillContainer skillContainer) =>
            new SkillContainerDto(
                skills: skillContainer.GetSkills()
                    .Select(skill => new SkillDto(skill.id, skill.statusEffect))
                    .ToList(),
                MaxSkills: skillContainer.amountOfSkills
            );

        private UnitStatsDto TransferUnitStatsToDto(UnitStats stats) =>
            new UnitStatsDto(
                Health: stats.health,
                AvoidChance: stats.avoidChance,
                Armor: stats.armor,
                DamageMultiplier: stats.damageMultiplayer,
                AmountOfMoves: stats.amountOfMoves,
                AmountOfBattleMoves: stats.amountOfBattleMoves
            );

        private List<StatusEffectObjDto> TransferStatusEffectsToDto(List<StatusEffectObj> statusEffectObjs) =>
            statusEffectObjs
                .Select(effect => new StatusEffectObjDto(
                    IsFrendly: effect.isFrendly,
                    IsStacking: effect.isStacking,
                    StatusEffect: effect.statusEffect,
                    TurnsRemains: effect.turnsRamains,
                    Value: effect.value
                ))
                .ToList();

        public UnitDto UnitToJson(BaseUnit unitToStringify) =>
            new UnitDto(
                Id: unitToStringify.id,
                Race: unitToStringify.race,
                IsLeader: unitToStringify.isLeader,
                X: unitToStringify.x,
                Y: unitToStringify.y,
                MoveType: unitToStringify.moveType,
                State: unitToStringify._state.state,
                BehaviourType: unitToStringify.behaviour.behaviourType,
                Stats: TransferUnitStatsToDto(unitToStringify.stats),
                WeaponType: unitToStringify.weaponType,
                Inventory: TransferInventoryToDto(unitToStringify.inventory),
                SkillContainer: TransferSkillContainerToDto(unitToStringify.skillContainer),
                StatusEffects: TransferStatusEffectsToDto(unitToStringify.statusEffects)
            );
    }
}
