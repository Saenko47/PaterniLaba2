using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Factories
{
    internal abstract class BaseCreateUnit<T> where T : BaseUnit
    {

        public T CreateUnit(WeaponType type, MoveType moveType) 
        {
            if (type == WeaponType.Melee) return CreateMeleeUnit(moveType);
            else if (type == WeaponType.Range) return CreatRangeUnit(moveType);

            throw new Exception();
        }

        protected abstract T CreateMeleeUnit(MoveType move);

        protected abstract T CreatRangeUnit(MoveType move);
    }
}
