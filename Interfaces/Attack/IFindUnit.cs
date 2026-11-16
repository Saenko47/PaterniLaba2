using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Attack
{
    internal interface IFindUnit
    {
        List<BaseUnit> Find(ReqiestOnFindingUnit req);
    }
}
