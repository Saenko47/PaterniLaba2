using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Skills
{
    internal class HealSkill : BaseSkill
    {
        public override bool isPositive => true;
        public override int value => 25; // Количество восстанавливаемого здоровья
        public override int range => 3;  // Дальность применения
        public override bool isOneTimer => true;
        public override bool isStacking => false;
        public override int duration => 0; // Для одноразовых эффектов длительность не нужна

        public override StrategysToFind strategysToFind => StrategysToFind.Circle; // Поиск союзников
        public override StatusEffect statusEffect => StatusEffect.Health;

        public override bool PerformSkill(BaseUnit owner, List<BaseUnit> targets)
        {
            if (targets == null || targets.Count == 0) return false;

            foreach (var target in targets)
            {
                var request = new RequestOnRecievingStatusEffect
                {
                    statusEffects = statusEffect,
                    value = value,
                    duration = duration,
                    isOneTimer = isOneTimer,
                    isStacking = isStacking,
                    isFrendly = isPositive
                };

                target.RecieveStatusEffect(request);
            }

            return true;
        }
    }
}
