using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Items;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories
{
    internal class BaseCreateItem:IItemCreator
    {
        private readonly ICreateItem<BaseBoots> _createBoots;
        private readonly ICreateItem<BaseLeggins> _createLeggins;
        private readonly ICreateItem<BasePlate> _createPlate;
        private readonly ICreateItem<BaseHelmet> _createHelmet;
        private readonly ICreateItem<BaseJewery> _createJewery;

      
        public BaseCreateItem(
            ICreateItem<BaseBoots> createBoots,
            ICreateItem<BaseLeggins> createLeggins,
            ICreateItem<BasePlate> createPlate,
            ICreateItem<BaseHelmet> createHelmet,
            ICreateItem<BaseJewery> createJewery)
        {
            _createBoots = createBoots;
            _createLeggins = createLeggins;
            _createPlate = createPlate;
            _createHelmet = createHelmet;
            _createJewery = createJewery;
        }

        // Метод создания предмета на основе типа снаряжения и контекста
        public BaseItem CreateItemByType( ItemDto context)
        {
            BaseItem createdItem = context._itemType switch
            {
                ItemType.Boots => _createBoots?.CreateItem(context),
                ItemType.Leggings => _createLeggins?.CreateItem(context),
                ItemType.Chestplate => _createPlate?.CreateItem(context),
                ItemType.Helmet => _createHelmet?.CreateItem(context),
                ItemType.Jewery => _createJewery?.CreateItem(context),
                _ => throw new ArgumentOutOfRangeException(nameof(context._itemType), $"Unknown item type: {context._itemType}")
            };

            // Проверяем и приводим к типу T, если это необходимо
            if (createdItem is BaseItem resultItem)
            {
                return resultItem;
            }

            throw new InvalidOperationException($"Created item does not match type {typeof(BaseItem).Name}");
        }


    }
}
