using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Singltons;
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
            int id = UnitsIdCreator.CreateAndReturnId();

            if (type == WeaponType.Melee) return CreateMeleeUnit(id,moveType);
            else if (type == WeaponType.Range) return CreatRangeUnit(id,moveType);

            throw new Exception();
        }

        protected abstract T CreateMeleeUnit(int id,MoveType move);

        protected abstract T CreatRangeUnit(int id,MoveType move);
    }
}
