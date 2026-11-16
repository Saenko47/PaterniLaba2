using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Items
{
    internal abstract class BaseItem
    {
        public abstract int Id { get; protected set; }
        public abstract ItemType itemType { get; }
        public Dictionary<StatusEffect, int> buffs { get; protected set; } = new();
        public BaseItem() { }

        public virtual BaseItem Clone()
        {

            BaseItem clone = (BaseItem)this.MemberwiseClone();

            clone.buffs = new Dictionary<StatusEffect, int>();

            return clone;
        }



    }
}
