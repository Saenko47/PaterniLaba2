using PaterniLab2.Models.Base;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.LeaderGenerator
{
    internal abstract class BaseLeaderGenerator
    {
        protected abstract BaseWeapon _weaponOfLeader { get;}
        //will be skills of leader; 
        public BaseLeaderGenerator() { }

        public abstract void Elect(BaseUnit unitToElect);




    }
}
