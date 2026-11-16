using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Table;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Table;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Placement
{
    internal abstract class BasePlacementOfUnits 
    {
        protected ICreateUnitByRace _createUnitByRace;

        public BasePlacementOfUnits(ICreateUnitByRace createUnitByRace) 
        {
            _createUnitByRace = createUnitByRace;
        }
 

        protected BaseUnit? ChooseUnitToPlace(UnitTypeToPlace unitTypeToPlace, PlaceOnBoardRequest req)
        {
            switch (unitTypeToPlace)
            {
                case UnitTypeToPlace.Melee:
                    req.raceMeleeAmount--;
                    return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Melee, MoveType.Walk);
                case UnitTypeToPlace.CavalryMelee:
                    req.raceMeleeAmount--;
                    req.raceAmountRide--;
                    return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Melee, MoveType.Cavalry);
                case UnitTypeToPlace.Range:
                    req.raceRangeAmount--;
                    return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Range, MoveType.Walk);
                case UnitTypeToPlace.CavalryRange:
                    req.raceRangeAmount--;
                    req.raceAmountRide--;
                    return _createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Range, MoveType.Cavalry);
                default:
                    return null;
            }
        }

        protected void ChooseLeader(BaseUnit unitToLeader)
        {
            unitToLeader.ElectAsLeader();
        }

        public abstract Army PaceUnitsOnTheBoard(PlaceOnBoardRequest req, Cell[,] table);

    }
}
