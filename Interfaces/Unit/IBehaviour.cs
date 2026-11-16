using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface IBehaviour
    {
        BehaviourType behaviourType { get; }
        bool MakeMove(TableContext context, BaseUnit _owner);
    }
}
