using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Table;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools
{
    internal class PlaceCommand : IPlaceCommand
    {
        private Vector2Int _prevPos;
        private Vector2Int _currentPos;

        private BaseUnit _unit;

        private Cell[,] _table;
        private PlaceOnBoardRequest _req;
        private UnitTypeToPlace _unitType;
        private List<BaseUnit> _placedUnits;

        public PlaceCommand(BaseUnit unit, Vector2Int currentPos, Cell[,] table, PlaceOnBoardRequest req, UnitTypeToPlace unitType, List<BaseUnit> placedUnits)
        {
            _unit = unit;
            _prevPos = unit.Position;
            _currentPos = currentPos;
            _table = table;
            _req = req;
            _unitType = unitType;
            _placedUnits = placedUnits;
        }
        public void Execute()
        {
                if(_unit == null || _table == null)    
                    throw new ArgumentNullException("Unit or table is null");


                _table[_currentPos.X, _currentPos.Y].isEmpty = false;
                _unit.SetPosition(_currentPos.X, _currentPos.Y);
            
        }

        public void Undo()
        {
            if (_unit == null || _table == null)
                throw new ArgumentNullException("Unit or table is null");
            _table[_currentPos.X, _currentPos.Y].isEmpty = true;
            _placedUnits.Remove(_unit);

            switch (_unitType)
            {
                case UnitTypeToPlace.Melee:
                    _req.raceMeleeAmount++;
                    break;
                case UnitTypeToPlace.CavalryMelee:
                    _req.raceMeleeAmount++;
                    _req.raceAmountRide++;
                    break;
                case UnitTypeToPlace.Range:
                    _req.raceRangeAmount++;
                    break;
                case UnitTypeToPlace.CavalryRange:
                    _req.raceRangeAmount++;
                    _req.raceAmountRide++;
                    break;
            }
        }
    }
}
