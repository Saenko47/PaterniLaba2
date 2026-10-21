using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Elfs
{
    internal class ElfBoots:BaseBoots
    {
        private const int ARMOR_BUFF = 2;
        public ElfBoots()
        {
            base.buffs.Add(BuffType.Armor, ARMOR_BUFF);
        }
    }
}
