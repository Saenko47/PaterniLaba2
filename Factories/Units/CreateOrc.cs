using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Orcs;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories.Units
{
    internal class CreateOrc: BaseCreateUnit<OrcUnit>
    {
      protected override OrcUnit CreateMeleeUnit(MoveType move)
        {
            var newOrc =  new OrcUnit(20, move,new BattleAxe(), new Inventory(3));
            newOrc.SetBehaviour(new UnitBehaviour(newOrc, new AiMoveHandler(newOrc, new Move()), new Attack(new DamageCalculator()), new FindEnemyToAttack()));
            return newOrc;
        }
        protected override OrcUnit CreatRangeUnit(MoveType move)
        {
            var newOrc = new OrcUnit(10, move, new Sling(), new Inventory(3));
            newOrc.SetBehaviour(new UnitBehaviour(newOrc, new AiMoveHandler(newOrc, new Move()), new Attack(new DamageCalculator()), new FindEnemyToAttack()));
            return newOrc;
        }
    }
}
