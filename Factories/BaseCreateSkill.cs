using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Skills;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories
{
    internal class BaseCreateSkill:ISkillCreator
    {
        private readonly ICreateSkill _createHeathSkill;
        private readonly ICreateSkill _createArmorSkill;
        private readonly ICreateSkill _createAvoidChanceSkill;
        private readonly ICreateSkill _createDamageSkill;

        public BaseCreateSkill(ICreateSkill createHeathSkill, ICreateSkill createArmorSkill, ICreateSkill createAvoidChanceSkill, ICreateSkill createDamageSkill) 
        {
            _createHeathSkill = createHeathSkill;
            _createArmorSkill = createArmorSkill;
            _createAvoidChanceSkill = createAvoidChanceSkill;
            _createDamageSkill = createDamageSkill;
        }

        public BaseSkill CreateSkill(SkillDto skillDto) 
        {
            ICreateSkill skillCreator;

            switch (skillDto.statusEffect) 
            {
                case StatusEffect.Health:
                    skillCreator = _createHeathSkill;
                    break;
                case StatusEffect.Armor:
                    skillCreator = _createArmorSkill;
                    break;
                case StatusEffect.AvoidChance:
                    skillCreator = _createAvoidChanceSkill;
                    break;
                case StatusEffect.Attack:
                    skillCreator = _createDamageSkill;
                    break;
                default: throw new Exception("Thres no such skill!!!!!");
            
            }
            return skillCreator.CreateSkill(skillDto.id);
        }
    }
}
