using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Orcs
{
    internal class Sling:BaseRange
    {
        private const int _damage = 10;
        private const int _armorPierce = 5;
        private const int _range = 3;
        public Sling() 
        {
           this.damage = _damage;
           this.armorPierce = _armorPierce;
           this.range = _range;
        }
    }
}
