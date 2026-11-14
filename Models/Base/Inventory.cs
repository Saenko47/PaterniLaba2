using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base
{
    internal class Inventory:IInventory
    {
        public BaseHelmet _helmet { get; private set; } = null;

        public BasePlate _plate { get; private set; } = null;

        public BaseLeggins _leggins { get; private set; } = null;

        public BaseBoots _boots { get; private set; } = null;

        public List<BaseJewery> _jewery { get; private set; } = new List<BaseJewery>();

        private int _jeweryMaxCount;

        public Inventory() { }
        public Inventory(int jeweryMaxCount) 
        {
            _jeweryMaxCount = jeweryMaxCount;
           
        }

        public void SetHelmet(BaseHelmet helmet)
        {
            if (helmet != null) _helmet = helmet;
        }

        public void SetPlate(BasePlate plate) 
        {
            if (plate != null) _plate = plate;
        }

        public void SetLeggins(BaseLeggins leggins) 
        {
            if (leggins != null) _leggins = leggins;
        }

        public void SetBoot(BaseBoots boots) 
        {
            if (boots != null) _boots = boots;
        }

        public void AddJewery(BaseJewery jewery) 
        {
            if (_jewery.Count() < _jeweryMaxCount) _jewery.Add(jewery);
        }
        public int GetMaxJewery() 
        {
            return _jeweryMaxCount;
        }

        public IEnumerable<BaseItem> GetAllEquippedItems()
        {
            if (_helmet != null) yield return _helmet;
            if (_plate != null) yield return _plate;
            if (_leggins != null) yield return _leggins;
            if (_boots != null) yield return _boots;

            foreach (var jewery in _jewery)
            {
                if (jewery != null) yield return jewery;
            }
        }

        public void SetItem(BaseItem itemToSet) 
        {
            switch (itemToSet.itemType) 
            {
                case ItemType.Boots: SetBoot((BaseBoots)itemToSet); break;
                case ItemType.Chestplate: SetPlate((BasePlate)itemToSet); break;
                case ItemType.Leggings: SetLeggins((BaseLeggins)itemToSet);break;
                case ItemType.Helmet: SetHelmet((BaseHelmet)itemToSet); break;
                case ItemType.Jewery:AddJewery((BaseJewery)itemToSet);break;
                default: throw new Exception("Thres no such ietmType!!!");
            }
        }


        public IInventory Clone()
        {
        
            Inventory clone = (Inventory)this.MemberwiseClone();

          
            clone._helmet = (BaseHelmet)_helmet?.Clone();
            clone._plate = (BasePlate)_plate?.Clone();
            clone._leggins = (BaseLeggins)_leggins?.Clone();
            clone._boots = (BaseBoots)_boots?.Clone();

          
            clone._jewery = new List<BaseJewery>();
            foreach (var jewery in this._jewery)
            {
                if (jewery != null)
                {
                    clone._jewery.Add((BaseJewery)jewery.Clone());
                }
            }

            return clone;
        }
    }
}
