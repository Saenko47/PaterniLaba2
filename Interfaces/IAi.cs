using PaterniLab2.Models;
using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IAi
    {
        public void MakeMove(BaseUnit unit, Table table, List<BaseUnit> enemyUnits, Action<BaseUnit> onUnitDeath);
    }
}
