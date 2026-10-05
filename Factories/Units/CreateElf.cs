using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Units
{
    internal class CreateElf
    {
        public ElfUnit CreateElfUnit(CreateUnitRequest request) 
        { 
       var newElfUnit = new ElfUnit(request.health, request.avaidChance, request.armor, request.moveType, request.weapons);
            return newElfUnit;
        }
    }
}
