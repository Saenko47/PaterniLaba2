using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools.PipelineSteps.Skills
{
    internal class CastSkill:IActionStep
    {
        public bool Execute(ActionContext actionContext, TableContext tableContext)
        {
            var reqToApplySkill = new RequestOnRecievingStatusEffect { duration = actionContext.skillToCast.duration,
                isFrendly = actionContext.skillToCast.isPositive,
                statusEffects = actionContext.skillToCast.statusEffect,
                isStacking = actionContext.skillToCast.isStacking,
                isOneTimer = actionContext.skillToCast.isOneTimer,
                value = actionContext.skillToCast.value };
            foreach (var t in actionContext.target) t.RecieveStatusEffect(reqToApplySkill);
            return true;
        }
    }
}
