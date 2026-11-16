using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class UnitDto
    {
        public int Id { get; set; }
        public Race Race { get; set; }
        public bool IsLeader { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public MoveType MoveType { get; set; }
        public UnitStateEnum state { get; set; }
        public BehaviourType behaviourType { get; set; }

        // Статы
        public UnitStatsDto Stats { get; set; }

        // Оружие и инвентарь
        public WeaponType WeaponType { get; set; }
        public InventoryDto Inventory { get; set; }

        // Скиллы
        public SkillContainerDto SkillContainer { get; set; }

        // Эффекты
        public List<StatusEffectObjDto> StatusEffects { get; set; } = new();
    }
}
