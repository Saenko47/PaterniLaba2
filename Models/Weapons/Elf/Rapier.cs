using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Elf
{
    internal class Rapier:BaseMelee
    {
        private const int _damage = 10;
        private const int _armorPierce = 15;
        public Rapier()
        {
            this.damage = _damage;
            this.armorPierce = _armorPierce;
        }
    }
}
