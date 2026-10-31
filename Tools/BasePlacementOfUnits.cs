using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Table;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal abstract class BasePlacementOfUnits 
    {
        protected ICreateUnitByRace _createUnitByRace;

        public BasePlacementOfUnits(ICreateUnitByRace createUnitByRace) 
        {
            _createUnitByRace = createUnitByRace;
        }
 

        protected BaseUnit? ChooseUnitToPlace(PlaceOnBoardRequest req)
        {
            if (req.raceMeleeAmount > 0)
            {
                req.raceMeleeAmount--;
                return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Melee, MoveType.Walk);
            }

            if (req.raceRangeAmount > 0)
            {
                req.raceRangeAmount--;
                return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Range, MoveType.Walk);
            }

            return null; 
        }

        protected void ChooseLeader(BaseUnit unitToLeader)
        {
            switch (unitToLeader.race)
            {
                case Race.Human:
                    HumanLeader.Instance.Elect(unitToLeader);
                    break;
                case Race.Elf:
                    ElfLeader.Instance.Elect(unitToLeader);
                    break;
                case Race.Orc:
                    OrcLeader.Instance.Elect(unitToLeader);
                    break;
            }
        }

        public abstract Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req, Cell[,] table);

    }
}
