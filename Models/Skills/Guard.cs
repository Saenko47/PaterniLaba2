using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Skills
{
    internal class Guard: BaseSkill
    {
        public override bool isPositive => true;
        public override int value => 25; // Количество восстанавливаемого здоровья
        public override int range => 0;  // Дальность применения
        public override bool isOneTimer => false;
        public override bool isStacking => false;
        public override int duration => 2; // Для одноразовых эффектов длительность не нужна

        public override StrategysToFind strategysToFind => StrategysToFind.Yourself; // Поиск союзников
        public override int id => 1;
        public override StatusEffect statusEffect => StatusEffect.Armor;

        public override bool PerformSkill(BaseUnit owner, List<BaseUnit> targets) 
        {
            if (owner == null) return false;

            var request = new RequestOnRecievingStatusEffect
            {
                statusEffects = statusEffect,
                value = value,
                duration = duration,
                isOneTimer = isOneTimer,
                isStacking = isStacking,
                isFrendly = isPositive
            };

            owner.RecieveStatusEffect(request);

            return true;
        }
    }
}
