using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Attack
{
    internal interface IAttack
    {
        bool AttackUnit(BaseUnit attacker, BaseUnit defender);
    }
}
