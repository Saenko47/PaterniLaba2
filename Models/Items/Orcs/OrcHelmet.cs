using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Orc
{
    internal class OrcHelmet:BaseHelmet
    {
        private const int ARMOR_BUFF = 5;
        public OrcHelmet() 
        {
            base.buffs.Add(BuffType.Armor, ARMOR_BUFF);
        }
    }
}
