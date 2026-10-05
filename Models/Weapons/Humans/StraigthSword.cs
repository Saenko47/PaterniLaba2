using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Humans
{
    internal class StraigthSword:BaseMelee
    {
        private const int _damage = 15;
        private const int _armorPierce = 5;
        public StraigthSword() 
        {
           this.damage = _damage;
           this.armorPierce = _armorPierce;
        }
    }
}
