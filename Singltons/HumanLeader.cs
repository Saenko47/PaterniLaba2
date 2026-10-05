using PaterniLab2.Models.Base;
using PaterniLab2.Models.Weapons.Humans;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class HumanLeader
    {
        private static readonly HumanLeader _instance = new HumanLeader();

        private BaseUnit? _unit;


        private HumanArtefact humanArefact = new HumanArtefact();

        private HumanLeader() { }

   
        public static HumanLeader Instance => _instance;

        public void Elect(BaseUnit unit)
        {
            _unit = unit;
            _unit.GiveWeaponToUnit(humanArefact);
        }

        public BaseUnit GetLeader()
        {
            return _unit ?? throw new InvalidOperationException("There's no leader elected yet.");
        }

    }
}
