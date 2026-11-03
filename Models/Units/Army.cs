using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    internal class Army
    {
        public List<BaseUnit> _units { get; private set; }

        public List<BaseUnit> unitsToMoveLater { get; private set; }

        private BaseUnit? prevMovin = null;


        public Race _race { get; private set; } 
        public bool _isLeaderAlive { get; private set; } = true;
        public bool _isAi { get; private set; }
        public int currentIndex { get; private set; }

        public bool HasUnits => _units != null && _units.Count > 0;

        public Army() { }

        public Army(Race race,List<BaseUnit> units, bool isAi) 
        {
            _units = units;
            _race = race;
            _isAi = isAi;
        }

        public void AddToTheArmy(List<BaseUnit> units)
        {
            if (units == null || units.Count == 0) return;

            _units.AddRange(units);
        }

        public void RemoveUnit(BaseUnit unit) 
        {
            if (unit == null) return;
            _units.Remove(unit);
        }

        public void KillLeader() 
        {
            _isLeaderAlive = false;
        }
        public void IncrementIndex() 
        {
            currentIndex++;
        }

        public BaseUnit? GetUnitToMove()
        {
           
            var unit = _units.LastOrDefault(u => u._state.state == UnitStateEnum.Active);

            if (unit != null)
            {
        
                return unit;
            }

            unit = _units.LastOrDefault(u => u._state.state == UnitStateEnum.Delayed && u != prevMovin);

         
            if (unit == null && prevMovin != null)
            {
                unit = _units.LastOrDefault(u => u._state.state == UnitStateEnum.Delayed);
                if (unit == prevMovin) return null;
               
            }

          
            prevMovin = unit;

            return unit;
        }

        public void AddUnitInQueueForMove(BaseUnit unit) 
        {
            if (!_units.Contains(unit)) unitsToMoveLater.Add(unit);
        }

        public bool IsAllMakeMoves() 
        {
            return _units.All(u => u._state.state == UnitStateEnum.Unactive);
        }
        public void PrepareForNewRound() 
        {
            foreach (var unit in _units) unit.RenewState();
        } 
    }
}
