using PaterniLab2.Models.Base;
using PaterniLab2.Models.Weapons.Orcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class OrcLeader
    {
        private static readonly OrcLeader _instance = new OrcLeader();

        private BaseUnit? _unit;


        private OrcArtefact orcArefact = new OrcArtefact();

        private OrcLeader() { }


        public static OrcLeader Instance => _instance;

        public void Elect(BaseUnit unit)
        {
            _unit = unit;
            _unit.ElectAsLeader();
            _unit.GiveWeaponToUnit(orcArefact);
        }

        public BaseUnit GetLeader()
        {
            return _unit ?? throw new InvalidOperationException("There's no leader elected yet.");
        }
    }
}
