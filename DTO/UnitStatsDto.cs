using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public record UnitStatsDto(
     int Health,
     int AvoidChance,
     int Armor,
     int DamageMultiplier,
     int AmountOfMoves,
     int AmountOfBattleMoves
 );
}
