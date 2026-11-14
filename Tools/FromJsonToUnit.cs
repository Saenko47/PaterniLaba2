using PaterniLab2.DTO;
using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Factory.Skills;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Services.Behaviour;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class FromJsonToUnit
    {
        private readonly ICreateUnitByRace _createUnitByRace;
        private readonly IItemCreator _itemCreator;
        private readonly ISkillCreator _skillCreator;

        private readonly IBehaviour _aiBehavior;
        private readonly IBehaviour _playerBehaviour;

        public FromJsonToUnit(
            ICreateUnitByRace createUnitByRace,
            IItemCreator itemCreator,
            ISkillCreator skillCreator,
            IBehaviour aiBehavior,
            IBehaviour playerBehaviour)
        {
            _createUnitByRace = createUnitByRace;
            _itemCreator = itemCreator;
            _skillCreator = skillCreator;
            _aiBehavior = aiBehavior;
            _playerBehaviour = playerBehaviour;
        }

        private IBehaviour SetBehaviourForUnit(BehaviourType behaviourType) 
        {
            switch (behaviourType) 
            {
                case BehaviourType.Ai: return _aiBehavior;
                case BehaviourType.Player: return _playerBehaviour;
                default: throw new Exception();
            }
        }

        private void SetUnitInventory(List<ItemDto> itemDtos, IInventory inventory) 
        {
            foreach (var itemDto in itemDtos) 
            {
                var item = _itemCreator.CreateItemByType(itemDto);
                inventory.SetItem(item);
            }
        }
        private void SetUnitSkills(List<SkillDto> skillsDtos, ISkillContainer skillContainer) 
        {
            foreach (var skillDto in skillsDtos) 
            {
                var skill = _skillCreator.CreateSkill(skillDto);
                skillContainer.AddSkill(skill);
            }
        }
       

        public BaseUnit ConvertFromJsonToUnit(UnitDto unitDto) 
        {
            var unit = _createUnitByRace.CreateByRaceAndWeapon(unitDto.Race, unitDto.WeaponType, unitDto.MoveType);
            SetUnitInventory(unitDto.Inventory.items, unit.inventory);
            SetUnitSkills(unitDto.SkillContainer.skills, unit.skillContainer);
            unit.SetBehaviour(SetBehaviourForUnit(unitDto.behaviourType));
            return unit;

           
        }
    }
}
