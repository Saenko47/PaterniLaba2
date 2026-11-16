using PaterniLab2.Models.Base.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Items.Elfs
{
    internal class ElfPlate:BasePlate
    {
        public override int Id => 1;
        private const int ARMOR_BUFF = 10;
        public ElfPlate()
        {
            base.buffs.Add(StatusEffect.Armor, ARMOR_BUFF);
        }
    }
}
