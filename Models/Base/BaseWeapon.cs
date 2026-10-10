using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base
{
    internal abstract class BaseWeapon: IClone<BaseWeapon>
    {
        public abstract WeaponType weaponType { get; set; }
        public int damage { get; protected set; }
        public int armorPierce { get; protected set; }

        public abstract int range { get; protected set; }
        public int radiusOfAttack { get; protected set; } = 0;

        public BaseWeapon Clone()
        {
            return (BaseWeapon)this.MemberwiseClone();
        }


    }
}
