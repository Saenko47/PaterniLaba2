using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Race { get; set; }
        public bool IsLeader { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string MoveType { get; set; }

        // Статы
        public UnitStatsDto Stats { get; set; }

        // Оружие и инвентарь
        public WeaponDto Weapon { get; set; }
        public InventoryDto Inventory { get; set; }

        // Скиллы
        public SkillContainerDto SkillContainer { get; set; }

        // Эффекты
        public List<StatusEffectObjDto> StatusEffects { get; set; } = new();
    }
}
