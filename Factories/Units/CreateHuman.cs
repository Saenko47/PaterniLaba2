using PaterniLab2.Interfaces.Factory;
using PaterniLab2.Models.Base;
using PaterniLab2.Models.Units;
using PaterniLab2.Models.Weapons.Humans;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories.Units
{
    internal class CreateHuman : BaseCreateUnit<HumanUnit>
    {
     
        
        protected override HumanUnit CreateMeleeUnit(MoveType move)
        {
            return new HumanUnit(15,move, new StraigthSword());
        }
        protected override HumanUnit CreatRangeUnit(MoveType move)
        {
            return new HumanUnit(7, move, new Crossbow());
        }
    }
}
