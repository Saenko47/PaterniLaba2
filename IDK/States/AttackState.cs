using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.IDK.States
{
    internal class AttackState
    {

        public UnitStateEnum state => UnitStateEnum.Active;
        private readonly IAttack _attack;

        public AttackState(IAttack attack)
        {
            _attack = attack;
        }

        public bool MakeMove(CombatContext context, BaseUnit unit)
        {

            
        }

        public void OnEnter(BaseUnit unit)
        {
            return;
        }

        public void OnExit(BaseUnit unit)
        {
            return;
        }
    }
}
