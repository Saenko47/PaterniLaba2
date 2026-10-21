using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Orcs
{
    internal class OrcsBoots:BaseBoots
    {
        private const int ARMOR_BUFF = 2;
        public OrcsBoots()
        {
            base.buffs.Add(BuffType.Armor, ARMOR_BUFF);
        }
    }
}
