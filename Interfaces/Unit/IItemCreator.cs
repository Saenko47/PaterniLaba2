using PaterniLab2.DTO;
using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface IItemCreator
    {
        BaseItem CreateItemByType(ItemDto context);
    }
}
