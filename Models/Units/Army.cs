using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Units
{
    internal class Army:IClone<Army>
    {
        public List<BaseUnit> _units { get; private set; }

        public List<BaseUnit> unitsToMoveLater { get; private set; }

        private BaseUnit? prevMovin = null;


        public Race _race { get; private set; } 
        public bool _isLeaderAlive { get; private set; } = true;
        public bool _isAi { get; private set; }
        public int currentIndex { get; private set; }

        public bool HasUnits => _units != null && _units.Count > 0;

        [JsonIgnore]
        public Action<BaseUnit> OnUnitDies;

        

        public Army() { }

        public Army(Race race,List<BaseUnit> units, bool isAi) 
        {
            _units = units;
            _race = race;
            _isAi = isAi;
            
            SubscribeToUnitEvents(units);
        }

       
        private void SubscribeToUnitEvents(List<BaseUnit> units)
        {
            foreach (var unit in units)
            {
                unit.OnUnitHit -= HandleUnitHit;
                unit.OnUnitHit += HandleUnitHit;
            }
        }

        public void AddToTheArmy(List<BaseUnit> units)
        {
            if (units == null || units.Count == 0) return;

            _units.AddRange(units);
            SubscribeToUnitEvents(units);
        }
        public void AddToTheArmy(BaseUnit unit) 
        {
            if (unit == null) return;
            _units.Add(unit);
            SubscribeToUnitEvents(new List<BaseUnit>() { unit });
        }

        public void RemoveUnit(BaseUnit unit) 
        {
            if (unit == null) return;
            unit.OnUnitHit -= HandleUnitHit;
            _units.Remove(unit);
        }

        public void HandleUnitHit(BaseUnit unit)
        {

            if (!unit.isAlive) 
            { 
                if(unit.isLeader)
                {
                    KillLeader();
                }
                RemoveUnit(unit);
                OnUnitDies?.Invoke(unit);
            }

            

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
            foreach (var unit in _units) unit.PrepareUnitForNewTurn();
        }

        public Army Clone()
        {
          
            Army clone = (Army)this.MemberwiseClone();

          
            Dictionary<BaseUnit, BaseUnit> unitMapping = new Dictionary<BaseUnit, BaseUnit>();

            
            clone._units = new List<BaseUnit>();
            foreach (var unit in this._units)
            {
                if (unit != null)
                {
                    BaseUnit clonedUnit = unit.Clone();
                    clone._units.Add(clonedUnit);
                    unitMapping[unit] = clonedUnit; 
                }
            }

         
            clone.SubscribeToUnitEvents(clone._units);

           
            clone.unitsToMoveLater = new List<BaseUnit>();
            if (this.unitsToMoveLater != null)
            {
                foreach (var unit in this.unitsToMoveLater)
                {
                    if (unit != null && unitMapping.ContainsKey(unit))
                    {
                        clone.unitsToMoveLater.Add(unitMapping[unit]);
                    }
                }
            }

           
            if (this.prevMovin != null && unitMapping.ContainsKey(this.prevMovin))
            {
                clone.prevMovin = unitMapping[this.prevMovin];
            }
            else
            {
                clone.prevMovin = null;
            }

          
            clone.OnUnitDies = null;

            return clone;
        }
    }
}
