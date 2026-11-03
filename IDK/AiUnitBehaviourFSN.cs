using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.IDK
{
    internal class AiUnitBehaviourFSN
    {
        private readonly IMoveHandler _move;
        private readonly IAttack _attack;

        private readonly IFindUnit _findToAttack;

        public AiUnitBehaviourFSN(IMoveHandler move, IAttack attack, IFindUnit findToAttack)
        {
            _move = move;
            _attack = attack;
            _findToAttack = findToAttack;

        }


    }
}
