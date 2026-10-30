using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IUnitStats
    {
        public void AddHealth(int amount);

        public void AddAvoidChance(int amount);

        public void AddArmor(int amount);

        public void AddMultiplayerToDamage(int amount);

        public void AddMoves(int amount);

        public void AddBattleMoves(int amount);
    }
}
