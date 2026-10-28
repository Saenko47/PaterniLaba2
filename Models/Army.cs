using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models
{
    internal class Army
    {
        public List<BaseUnit> _units { get; private set; }


        public Race _race { get; private set; } 
        public bool _isLeaderAlive { get; private set; } = true;
        public bool _isAi { get; private set; }
        public int currentIndex { get; private set; }

        public bool HasUnits => _units != null && _units.Count > 0;

        public Army(Race race,List<BaseUnit> units, bool isAi) 
        {
            _units = units;
            _race = race;
            _isAi = isAi;
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
    }
}
