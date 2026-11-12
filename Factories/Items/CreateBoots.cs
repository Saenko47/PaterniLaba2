using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Items;
using PaterniLab2.Models.Base.Items;
using PaterniLab2.Models.Items.Elfs;
using PaterniLab2.Models.Items.Humans;
using PaterniLab2.Models.Items.Orcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Items
{
    internal class CreateBoots:ICreateItem<BaseBoots>
    {
        public BaseBoots CreateItem(ItemCreatingContext context) 
        {
            switch (context._id) 
            {
                case 1: return new ElfBoots();
                case 2: return new HumanBoots();
                case 3: return new OrcsBoots();
                default: throw new Exception("Theres no such item b ID!!!!");
            }
           
        
        }
    }
}
