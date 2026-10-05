using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Base
{
    internal class BaseMelee:BaseWeapon
    {
        public override int range { get; protected set; } = 1;
        
    }
}
