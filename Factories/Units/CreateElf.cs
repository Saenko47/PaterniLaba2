using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Elf;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories.Units
{
    internal class CreateElf:BaseCreateUnit<ElfUnit>
    {
      protected override ElfUnit CreateMeleeUnit(int id, MoveType move)
        {
           
            var newElf = new ElfUnit(id,10, move, new Rapier(), new Inventory(3));
            newElf.SetBehaviour(new AiUnitBehaviour(newElf, new AiMoveHandler(newElf, new Move()), new Attack(new DamageCalculator()), new FindUnit()));
            return newElf;
        }
        protected override ElfUnit CreatRangeUnit(int id, MoveType move)
        {
            var newElf = new ElfUnit(id,5, move, new LongBow(), new Inventory(3));
            newElf.SetBehaviour(new AiUnitBehaviour(newElf, new AiMoveHandler(newElf, new Move()), new Attack(new DamageCalculator()), new FindUnit()));
            return newElf;
        }
    }
}
