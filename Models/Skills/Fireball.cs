using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Skills
{
    internal class FireballSkill : BaseSkill
    {
        public override bool isPositive => false; // Флаг false укажет, что эффект негативный (certify = -1)
        public override int value => 30; // Количество урона
        public override int range => 4;  // Дальность применения
        public override bool isOneTimer => true;
        public override bool isStacking => false;
        public override int duration => 0;

        public override StrategysToFind strategysToFind => StrategysToFind.Circle; // Поиск врагов
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
