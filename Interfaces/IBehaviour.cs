using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IBehaviour
    {
        void MakeMove(List<BaseUnit> enemyUnits, Action<BaseUnit> onUnitDeath);
    }
}
