using PaterniLab2.Factories;
using PaterniLab2.Factories.Units;
using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class CreateUnitByRace: ICreateUnitByRace
    {

        private readonly BaseCreateUnit<HumanUnit> humanFabric;
        private readonly BaseCreateUnit<OrcUnit> orcFabric;
        private readonly BaseCreateUnit<ElfUnit> elfFabric;

        public CreateUnitByRace(BaseCreateUnit<HumanUnit> humanFabric, BaseCreateUnit<OrcUnit> orcFabric, BaseCreateUnit<ElfUnit> elfFabric)
        {
            this.humanFabric = humanFabric;
            this.orcFabric = orcFabric;
            this.elfFabric = elfFabric;
        }

        public BaseUnit CreateByRaceAndWeapon(Race race, WeaponType weaponType, MoveType move) 
        {
            BaseUnit unit;
            switch (race) 
            {
                case Race.Human:
                    unit = humanFabric.CreateUnit(weaponType, move);
                    break;
                case Race.Orc:
                    unit = orcFabric.CreateUnit(weaponType, move);
                    break;
                case Race.Elf:
                    unit = elfFabric.CreateUnit(weaponType, move);
                    break;
                default:
                    throw new Exception("theres no such race!!");
                    break;
            }
            return unit;
        } 
    }
}
