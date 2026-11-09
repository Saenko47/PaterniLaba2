using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PaterniLab2.Tools.PipelineSteps.Attack
{
    internal class ChooseEnemieToAttackStep: IActionStep
    {
        public bool Execute(ActionContext actionContext, TableContext tableContext) 
        {

            var decision = GlobalInputOutput.Instance.Read(tableContext.armies,"Choose enemy to attack:");
            if (!int.TryParse(decision, out int selectedTargetIndex)
                || selectedTargetIndex < 1 || selectedTargetIndex > actionContext.target.Count)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer("Action Failed!!!");
                return false;
            }

            var res = actionContext.attackExecutionStrategy?.Invoke(
                actionContext.owner, actionContext.target[selectedTargetIndex-1]) ?? false; ;

            return res;


            
        }
    }
}
