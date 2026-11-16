using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Humans
{
    internal class HumanHelmet:BaseHelmet
    {
        public override int Id => 2;
        private const int ARMOR_BUFF = 5;
        public HumanHelmet() 
        {
            base.buffs.Add(StatusEffect.Armor, ARMOR_BUFF);
        }
    }
}
