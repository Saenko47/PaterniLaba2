using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Elf;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories.Units
{
    internal class CreateElf:BaseCreateUnit<ElfUnit>
    {
      protected override ElfUnit CreateMeleeUnit(MoveType move)
        {
            return new ElfUnit(10, move, new Rapier());
        }
        protected override ElfUnit CreatRangeUnit(MoveType move)
        {
            return new ElfUnit(5, move, new LongBow());
        }
    }
}
