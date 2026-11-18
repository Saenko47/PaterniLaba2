using PaterniLab2.DTO;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class ArmyStringify
    {

        private List<UnitDto> GetAllUnitsToDto(List<BaseUnit> units) 
        {
            var unitStrinfy = new UnitStringify();
            var unitsDto = units.Select(u => unitStrinfy.UnitToJson(u)).ToList();
            return unitsDto;
        }
        public ArmyDto ArmyToDto(Army armyToDto) 
        {
           

            ArmyDto armyDto = new ArmyDto(armyToDto._race, armyToDto._isAi, GetAllUnitsToDto(armyToDto._units));

            return armyDto;
            
        }
    }
}
