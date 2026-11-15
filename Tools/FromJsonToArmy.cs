using PaterniLab2.DTO;
using PaterniLab2.Factories;
using PaterniLab2.Factories.Items;
using PaterniLab2.Factories.Skills;
using PaterniLab2.Factories.Units;
using PaterniLab2.Models.Units;
using PaterniLab2.Services.AttackAndDamageProccesing;
using PaterniLab2.Services.Behaviour;
using PaterniLab2.Services.MoveHandlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class FromJsonToArmy
    {
        private FromJsonToUnit SetCreator() 
        {
            var unitGenerator = new CreateUnitByRace(new CreateHuman(), new CreateOrc(), new CreateElf());
            var itemGenerator = new BaseCreateItem(new CreateBoots(), new CreateLeggins(), new CreateChestPlate(), new CreateHelmet(), new CreateJewery());
            var skillCreator = new BaseCreateSkill(new CreateHealthSkill(), new CreateArmorSkill(), new CreateAvoidChanceSkill(), new CreateAttackSkill());
            var stateCreator = new BaseCreateUnitState();

            AiUnitBehaviour aiBeha = new AiUnitBehaviour(new AiMoveHandler(new Move()), new Attack(new DamageCalculator()), new FindUnit());
            PlayerUnitBehaviour humanBeha = new PlayerUnitBehaviour(new HumanKeyboardMoveInput(new Move()), new Attack(new DamageCalculator()), new FindUnit());

            var unitFromJsonCreator = new FromJsonToUnit(unitGenerator, itemGenerator,skillCreator, aiBeha, humanBeha);

            return unitFromJsonCreator;
        }

        public List<Army> ConvertFromJsonToArmy(List<ArmyDto> armieDtos) 
        {
            var unitGenerator = SetCreator();

            List<Army> armiesFromJson = new List<Army>();

            foreach (var armyDto in armieDtos) 
            {
                var units = armyDto.Units.Select(u => unitGenerator.ConvertFromJsonToUnit(u)).ToList();
                var race = armyDto.Race;
                var isAi = armyDto.IsAi;

                armiesFromJson.Add(new Army(race, units, isAi));

            }

            return armiesFromJson;

        }
    }
}
