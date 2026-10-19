using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Items
{
    internal abstract class BaseHelmet: BaseItem
    {
        public override ItemType itemType => ItemType.Helmet;


    }
}
