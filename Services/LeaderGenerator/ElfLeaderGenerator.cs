using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Weapons.Elf;
using PaterniLab2.Models.Weapons.Humans;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.LeaderGenerator
{
    internal class ElfLeaderGenerator:BaseLeaderGenerator
    {
        protected override BaseWeapon _weaponOfLeader => new ElfArtefact();

        public ElfLeaderGenerator() : base() { }

        public override void Elect(BaseUnit unitToElect)
        {
            unitToElect.GiveWeaponToUnit(_weaponOfLeader);
            unitToElect.isLeader = true;
        }
    }
}
