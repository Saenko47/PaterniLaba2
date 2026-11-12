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
    internal class CreateHelmet : ICreateItem<BaseHelmet>
    {
        public BaseHelmet CreateItem(ItemCreatingContext context)
        {
            switch (context._id) 
            {
                case 1: return new ElfHelmet();
                case 2: return new HumanHelmet();
                case 3: return new OrcHelmet();
                default: throw new Exception("Theres no such ID!!!!");
            }
        }
    }
}
