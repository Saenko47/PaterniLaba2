using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools.PipelineSteps.Skills
{
    internal class ChooseSkill: IActionStep
    {
        public bool Execute(ActionContext actionContext, TableContext tableContext)
        {
            var decision = GlobalInputOutput.Instance.Read(tableContext.armies, "Choose enemy to attack:");
            if (!int.TryParse(decision, out int selectedSkill)
                || selectedSkill < 0 || selectedSkill > actionContext.owner.skillContainer.amountOfSkills)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer("Action Failed!!!");
                return false;
            }

            ISkill skillToCast = actionContext.owner.skillContainer.GetSkillByKey(selectedSkill);

            actionContext.actionExecutionStrategy = skillToCast.PerformSkill;
            return true;
        }
    }
}
