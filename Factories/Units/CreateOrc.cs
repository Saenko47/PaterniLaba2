using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Units
{
    internal class CreateOrc
    {
        public OrcUnit CreateOrcUnit(CreateUnitRequest request)
        {
            var newOrcUnit = new OrcUnit(request.health, request.avaidChance, request.armor, request.moveType, request.weapons);
            return newOrcUnit;
        }
    }
}
