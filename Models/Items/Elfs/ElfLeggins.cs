using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Elfs
{
    internal class ElfLeggins:BaseLeggins
    {
        private const int ARMOR_BUFF = 5;
        public ElfLeggins() 
        {
            base.buffs.Add(ItemBuffType.Armor, ARMOR_BUFF);
        }
    }
}
