using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Weapons.Humans
{
    internal class HumanArtefact:BaseMelee
    {
        private const int _damage = 40;
        private const int _armorPierce = 20;
        public HumanArtefact()
        {
            this.damage = _damage;
            this.armorPierce = _armorPierce;
        }
    }
}
