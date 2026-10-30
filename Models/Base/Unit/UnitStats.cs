using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Models.Base.Unit
{
    internal class UnitStats
    {
        public int health { get; protected set; } = 100;
        public int avoidChance { get; protected set; } = 5;
        public int armor { get; protected set; } = 5;

        public int damageMultiplayer { get; protected set; } = 0;

        public int amountOfMoves { get; protected set; } = 1;
        public int amountOfBattleMoves { get; protected set; } = 1;


        public UnitStats() { } 

        public void ModifyHealth(int amount) => health = Math.Clamp(health + amount,0, int.MaxValue);

        public void ModifyAvoidChance(int amount) => avoidChance += amount;

        public void ModifyArmor(int amount) => armor += amount;

        public void ModifyMultiplayerToDamage(int amount) => damageMultiplayer += amount;

        public void ModifyMoves(int amount) => Math.Max(0,amountOfMoves += amount);

        public void ModifyBattleMoves(int amount) => Math.Max(0, amountOfMoves += amount);

    }
}
