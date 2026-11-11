using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Unit
{
    internal abstract class BaseSkill:IClone<BaseSkill>
    {
        public abstract bool isPositive { get; }
        public abstract int value { get; }
        public abstract int range { get; }
        public abstract bool isOneTimer { get; }
        public abstract bool isStacking { get; }
        public abstract int duration { get; }

        public abstract StrategysToFind strategysToFind { get;  }

        public abstract StatusEffect statusEffect { get; }
        public BaseSkill() { }

        public abstract bool PerformSkill(BaseUnit owner, List<BaseUnit> targets);

        public virtual BaseSkill Clone()
        {

            BaseSkill clone = (BaseSkill)this.MemberwiseClone();


            return clone;
        }



    }
}
