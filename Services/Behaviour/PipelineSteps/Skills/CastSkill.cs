using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.Behaviour.PipelineSteps.Skills
{
    internal class CastSkill:IActionStep
    {
        public bool Execute(ActionContext actionContext, TableContext tableContext)
        {
            if (actionContext == null 
                || tableContext == null 
                || actionContext.actionExecutionStrategy == null
                || actionContext.owner == null 
                || actionContext.target == null) return false;
            actionContext.actionExecutionStrategy?.Invoke(actionContext.owner, actionContext.target);
            return true;
        }
    }
}
