using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces
{
    internal interface ICreateUnitByRace
    {
        BaseUnit CreateByRaceAndWeapon(Race race, WeaponType weaponType, MoveType move);
    }
}
