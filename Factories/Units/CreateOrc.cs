using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Orcs;
using PaterniLab2.Requests.Units;
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
            return new OrcUnit(20, move,new BattleAxe());
        }
        protected override OrcUnit CreatRangeUnit(MoveType move)
        {
            return new OrcUnit(10, move, new Sling());
        }
    }
}
