using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IFSNStateAttack:IFSNState
    {
        void SetEnemyToAttack(BaseUnit unitToAttack);
    }
}
