using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Attack
{
    internal interface IDamageCalculator
    {
        int CalculateDamage(BaseUnit attacker, BaseUnit defender);
    }
}
