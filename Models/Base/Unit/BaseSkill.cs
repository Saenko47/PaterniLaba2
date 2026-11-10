using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Unit
{
    internal abstract class BaseSkill
    {
        public abstract bool isPositive { get; }
        public abstract int value { get; }
        public abstract int range { get; }
        public abstract bool isOneTimer { get; }
        public abstract bool isStacking { get; }
        public abstract int duration { get; }

        public abstract StrategysToFind strategysToFind { get;  }

        public abstract StatusEffect statusEffect { get; }

        public abstract bool PerformSkill(BaseUnit owner, List<BaseUnit> targets);

        

    }
}
