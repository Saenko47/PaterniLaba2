using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Orcs
{
    internal class OrcsBoots:BaseBoots
    {
        public override int Id => 3;
        private const int ARMOR_BUFF = 2;
        public OrcsBoots()
        {
            base.buffs.Add(StatusEffect.Armor, ARMOR_BUFF);
        }
    }
}
