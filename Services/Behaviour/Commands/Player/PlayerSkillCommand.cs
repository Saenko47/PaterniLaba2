using PaterniLab2.Interfaces.Commands;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour.Commands.Player
{
    internal class PlayerSkillCommand: UnitCommand
    {
        public PlayerSkillCommand() { }

        public ResultOfAction Enter(TableContext context, ActionContext actionContext) 
        {
            throw new NotImplementedException();
        }
    }
}
