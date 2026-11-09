using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Attack;
using PaterniLab2.Interfaces.Commands;
using PaterniLab2.Requests;
using PaterniLab2.Tools;
using PaterniLab2.Tools.PipelineSteps.Attack;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour.Commands.Player
{
    internal class NewPlayerAttackCommand: UnitCommand
    {
        private readonly IAttack _attack;
        

        public NewPlayerAttackCommand(IAttack attack)
        {
            _attack = attack;
        }

        private IActionPipeline SetAttackPipeline() 
        {
            IActionStep firtStep = new ShowEnemiesStep();
            IActionStep secondStep = new ChooseEnemieToAttackStep();
            List<IActionStep> steps = new List<IActionStep>() { firtStep, secondStep};
            return new ActionPipeline(steps);
        }

        public ResultOfAction Enter(TableContext tableContext, ActionContext actionContext) 
        {
            actionContext.attackExecutionStrategy = _attack.AttackUnit;
            var pipeline = SetAttackPipeline();
           
            var res = pipeline.StartPipeline(
                actionContext, tableContext) ? ResultOfAction.SuccessAction : ResultOfAction.FailToAttack;

            actionContext.owner.DecreamentMoves(res);

            return res;
        }

    }
}
