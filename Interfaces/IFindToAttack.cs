using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IFindToAttack
    {
        BaseUnit FindToAttack(BaseUnit unitThatLookingForPray, List<BaseUnit> enemyUnits);
    }
}
