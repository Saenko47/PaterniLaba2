using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class ItemCreatingContext
    {
         public int _id { get; set; }
         public ItemType _itemType { get; set; }

        public ItemCreatingContext(int id, ItemType itemType)
        {
            _id = id;
            _itemType = itemType;
        }
    }
}
