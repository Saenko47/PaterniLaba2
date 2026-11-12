using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class ItemCreatingContext
    {
        int _id;
        ItemType _itemType;

        ItemCreatingContext(int id, ItemType itemType)
        {
            _id = id;
            _itemType = itemType;
        }
    }
}
