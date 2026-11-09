using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Commands;
using PaterniLab2.Requests;
using PaterniLab2.Tools;
using PaterniLab2.Tools.PipelineSteps.Attack;
using PaterniLab2.Tools.PipelineSteps.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour.Commands.Player
{
    internal class CastingSpellCommand: UnitCommand
    {
        private IActionPipeline SetCastingPipeline()
        {
            IActionStep firtStep = new ShowSkillsOfUnitStep();
            IActionStep secondStep = new ChooseSkill();
            IActionStep thirsdStep = new ChooseTargetCast();
            IActionStep foutStep = new CastSkill();
            List<IActionStep> steps = new List<IActionStep>() { firtStep, secondStep, thirsdStep, foutStep };
            return new ActionPipeline(steps);
        }

        public ResultOfAction Enter(TableContext tableContext, ActionContext actionContext)
        {
            var pipeline = SetCastingPipeline();
            var res = pipeline.StartPipeline(
                actionContext, tableContext) ? ResultOfAction.SuccessAction : ResultOfAction.FailToAttack;

            actionContext.owner.DecreamentMoves(res);

            return res;
        }
    }
}
