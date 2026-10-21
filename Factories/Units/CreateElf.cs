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
      protected override ElfUnit CreateMeleeUnit(MoveType move)
        {
            var newElf = new ElfUnit(10, move, new Rapier(), new Inventory(3));
            newElf.SetBehaviour(new UnitBehaviour(newElf, new AiMoveHandler(newElf, new Move()), new Attack(new DamageCalculator()), new FindEnemyToAttack()));
            return newElf;
        }
        protected override ElfUnit CreatRangeUnit(MoveType move)
        {
            var newElf = new ElfUnit(5, move, new LongBow(), new Inventory(3));
            newElf.SetBehaviour(new UnitBehaviour(newElf, new AiMoveHandler(newElf, new Move()), new Attack(new DamageCalculator()), new FindEnemyToAttack()));
            return newElf;
        }
    }
}
