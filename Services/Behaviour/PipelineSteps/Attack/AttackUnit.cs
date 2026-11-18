using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.Behaviour.PipelineSteps.Attack
{
    internal class AttackUnit:IActionStep
    {
        public bool Execute(ActionContext actionContext, TableContext tableContext) 
        {
            if (actionContext.owner == null || actionContext.target == null) return false;

           return actionContext.actionExecutionStrategy?.Invoke(actionContext.owner,actionContext.target) ?? false;
        }
    }
}
