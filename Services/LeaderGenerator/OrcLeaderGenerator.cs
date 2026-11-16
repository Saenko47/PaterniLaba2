using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Weapons.Humans;
using PaterniLab2.Models.Weapons.Orcs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.LeaderGenerator
{
    internal class OrcLeaderGenerator:BaseLeaderGenerator
    {
        protected override BaseWeapon _weaponOfLeader => new OrcArtefact();

        public OrcLeaderGenerator() : base() { }

        public override void Elect(BaseUnit unitToElect)
        {
            unitToElect.GiveWeaponToUnit(_weaponOfLeader);
            unitToElect.isLeader = true;
        }
    }
}
