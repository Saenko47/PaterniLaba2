using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base
{
    internal abstract class BaseRange:BaseWeapon
    {
        public override WeaponType weaponType { get; set; } = WeaponType.Range;
        public override int range { get; protected set; } = 2;
      
    }
}
