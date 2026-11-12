using PaterniLab2.DTO;
using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Factory.Items
{
    internal interface ICreateItem<T> where T: BaseItem
    {
        T CreateItem(ItemCreatingContext context);
    }
}
