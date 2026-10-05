using PaterniLab2.Models.Base;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Units
{
    internal class CreateHuman
    {
        public HumanUnit CreateHumanUnit(CreateUnitRequest request) 
        { 
        var newHumanUnit = new HumanUnit(request.health, request.avaidChance, request.armor, request.moveType, request.weapons);
            return newHumanUnit;
        }
    }
}
