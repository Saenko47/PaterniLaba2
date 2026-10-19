using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IAi
    {
        public void MakeMove(BaseUnit unit, List<BaseUnit> enemyUnits, Action<BaseUnit> onUnitDeath);
    }
}
