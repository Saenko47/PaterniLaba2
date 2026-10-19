using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Items
{
    internal class BaseJewery:BaseItem
    {
        public override ItemType itemType => ItemType.Jewery;
        public BaseJewery() { }
    }
}
