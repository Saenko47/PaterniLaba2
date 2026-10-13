using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces
{
    internal interface IItem
    {
        Dictionary<ItemBuffType, int> Buffs { get; }
    }
}
