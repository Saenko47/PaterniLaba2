using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface IInventory: IClone<IInventory>
    {
        public void SetHelmet(BaseHelmet helmet);


        public void SetPlate(BasePlate plate);


        public void SetLeggins(BaseLeggins leggins);


        public void SetBoot(BaseBoots boots);


        public void AddJewery(BaseJewery jewery);

        public void RemoveJewery(BaseJewery jewery);
        IEnumerable<BaseItem> GetAllEquippedItems();


    }
}
