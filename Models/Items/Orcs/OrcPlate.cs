using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Orcs
{
    internal class OrcPlate:BasePlate
    {
        private const int ARMOR_BUFF = 10;
        public OrcPlate()
        {
            base.buffs.Add(ItemBuffType.Armor, ARMOR_BUFF);
        }
    }
}
