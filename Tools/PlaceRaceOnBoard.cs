using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using PaterniLab2.Requests.Table;
using PaterniLab2.Requests.Units;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class PlaceRaceOnBoard: IPlaceRaceOnBoard
    {
        private readonly ICreateUnitByRace _createUnitByRace;

        public PlaceRaceOnBoard(ICreateUnitByRace createUnitByRace) 
        {
            _createUnitByRace = createUnitByRace;
        }

        private BaseUnit? ChooseUnitToPlace(PlaceOnBoardRequest req)
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

            return null; // Обязательный дефолтный возврат, если юнитов нет
        }

        private void ChooseLeader(BaseUnit unitToLeader) 
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

        public List<BaseUnit> PlaceOnBoard(PlaceOnBoardRequest req, Cell[,] table)
        {
            if (req == null || table == null) return new List<BaseUnit>();

            int xLim = table.GetLength(0);
            int yLim = table.GetLength(1);

            if (xLim == 0 || yLim == 0) return new List<BaseUnit>();

            int y = (table[0, 0].isEmpty == true) ? 0 : yLim - 1;
            int stepY = (y == 0) ? 1 : -1;

            List<BaseUnit> placedUnits = new List<BaseUnit>();

            // Сначала собираем точное количество юнитов в список, основываясь на исходных данных
            List<BaseUnit> unitsToPlaceList = new List<BaseUnit>();
            for (int i = 0; i < req.raceMeleeAmount; i++)
            {
                unitsToPlaceList.Add(_createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Melee, MoveType.Walk));
            }
            for (int i = 0; i < req.raceRangeAmount; i++)
            {
                unitsToPlaceList.Add(_createUnitByRace.CreateByRaceAndWeapon(req.race, WeaponType.Range, MoveType.Walk));
            }

            int unitIndex = 0;

            // Теперь расставляем готовый список по свободным клеткам
            while (y >= 0 && y < yLim && unitIndex < unitsToPlaceList.Count)
            {
                for (int x = 0; x < xLim && unitIndex < unitsToPlaceList.Count; x++)
                {
                    if (table[x, y].isEmpty == true)
                    {
                        var unitToPlace = unitsToPlaceList[unitIndex];

                        if (placedUnits.Count == 0 && unitIndex == 0)
                        {
                            ChooseLeader(unitToPlace);
                        }

                        unitToPlace.SetPosition(x, y);
                        table[x, y].isEmpty = false;
                        placedUnits.Add(unitToPlace);

                        unitIndex++; // Переходим к следующему юниту только когда успешно поставили текущего
                    }
                }

                y += stepY;
            }
            return placedUnits;
        }
    }
}
