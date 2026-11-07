using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Weapons.Humans;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.LeaderGenerator
{
    internal class HumanLeaderGenerator:BaseLeaderGenerator
    {
        protected override BaseWeapon _weaponOfLeader => new HumanArtefact();

        public HumanLeaderGenerator() : base(){ }

        public override void Elect(BaseUnit unitToElect)
        {
            unitToElect.GiveWeaponToUnit(_weaponOfLeader);
            unitToElect.isLeader = true;
        }
    }
}
