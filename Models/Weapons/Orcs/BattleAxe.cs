using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Orcs
{
    internal class BattleAxe:BaseMelee
    {
        private const int _damage = 25;
        private const int _armorPierce = 15;
        public BattleAxe() 
        {
           this.damage = _damage;
           this.armorPierce = _armorPierce;
        }
    }
}
