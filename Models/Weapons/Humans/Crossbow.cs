using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Humans
{
    internal class Crossbow:BaseRange
    {
        private const int _damage = 20;
        private const int _armorPierce = 10;
        private const int _range = 3;
        public Crossbow() 
        {
           this.damage = _damage;
           this.armorPierce = _armorPierce;
           this.range = _range;
        }
    }
}
