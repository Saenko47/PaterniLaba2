using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Items.Humans;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Humans;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories.Units
{
    internal class CreateHuman : BaseCreateUnit<HumanUnit>
    {
     
        
        protected override HumanUnit CreateMeleeUnit(int id, MoveType move)
        {

            var newHuman = new HumanUnit(id,15,move, new StraigthSword(), new Inventory(3));
          
            return newHuman;
        }
        protected override HumanUnit CreatRangeUnit(int id, MoveType move)
        {
            var newHuman =  new HumanUnit(id,7, move, new Crossbow(), new Inventory(3));
            
            return newHuman;
        }
    }
}
