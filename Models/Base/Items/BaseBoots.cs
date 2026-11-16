using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Items
{
    internal class BaseBoots:BaseItem
    {
        public override int Id { get; protected set; }
        public override ItemType itemType => ItemType.Boots;
    }
}
