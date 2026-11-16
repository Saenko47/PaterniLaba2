using PaterniLab2.DTO;
using PaterniLab2.Factories;
using PaterniLab2.Factories.Items;
using PaterniLab2.Factories.Skills;
using PaterniLab2.Factories.Units;
using PaterniLab2.Models.Units;
using PaterniLab2.Services.AttackAndDamageProccesing;
using PaterniLab2.Services.Behaviour;
using PaterniLab2.Services.MoveHandlers;
using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class FromJsonToArmy
    {
        private readonly IFromJsonToUnit _fromJsonToUnit;

        public FromJsonToArmy(IFromJsonToUnit fromJsonToUnit) 
        {
            _fromJsonToUnit = fromJsonToUnit;
        }
     

        public List<Army> ConvertFromJsonToArmy(List<ArmyDto> armieDtos) 
        {

            List<Army> armiesFromJson = new List<Army>();

            foreach (var armyDto in armieDtos) 
            {
                var units = armyDto.Units.Select(u => _fromJsonToUnit.ConvertFromJsonToUnit(u)).ToList();
                var race = armyDto.Race;
                var isAi = armyDto.IsAi;

                armiesFromJson.Add(new Army(race, units, isAi));

            }

            return armiesFromJson;

        }
    }
}
