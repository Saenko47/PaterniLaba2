using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Items;
using PaterniLab2.Models.Base.Items;
using PaterniLab2.Models.Items.Elfs;
using PaterniLab2.Models.Items.Humans;
using PaterniLab2.Models.Items.Orc;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Items
{
    internal class CreateJewery: ICreateItem<BaseJewery>
    {
        public BaseJewery CreateItem(ItemDto context)
        {
            switch (context.Id)
            {
                
                default: throw new Exception("Theres no such ID!!!!");
            }
        }
    }
}
