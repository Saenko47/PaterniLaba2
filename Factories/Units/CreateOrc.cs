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
      protected override OrcUnit CreateMeleeUnit(int id, MoveType move)
        {
            var newOrc =  new OrcUnit(id,20, move,new BattleAxe(), new Inventory(3));
          
            return newOrc;
        }
        protected override OrcUnit CreatRangeUnit(int id, MoveType move)
        {
            var newOrc = new OrcUnit(id,10, move, new Sling(), new Inventory(3));
           
            return newOrc;
        }
    }
}
