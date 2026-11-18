using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public record UnitDto(
      int Id,
      Race Race,
      bool IsLeader,
      int X,
      int Y,
      MoveType MoveType,
      UnitStateEnum State,
      BehaviourType BehaviourType,
      UnitStatsDto Stats,
      WeaponType WeaponType,
      InventoryDto Inventory,
      SkillContainerDto SkillContainer,
      List<StatusEffectObjDto> StatusEffects
  );
}
