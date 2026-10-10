using PaterniLab2.Models.Base;
using PaterniLab2.Models.Weapons.Elf;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class ElfLeader
    {
        private static readonly ElfLeader _instance = new ElfLeader();

        private BaseUnit? _unit;


        private ElfArtefact elfArefact = new ElfArtefact();

        private ElfLeader() { }


        public static ElfLeader Instance => _instance;

        public void Elect(BaseUnit unit)
        {
          
            _unit = unit;
            _unit.ElectAsLeader();
            _unit.GiveWeaponToUnit(elfArefact);
        }

        public BaseUnit GetLeader()
        {
            return _unit ?? throw new InvalidOperationException("There's no leader elected yet.");
        }
    }
}
