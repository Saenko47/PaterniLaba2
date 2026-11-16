using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.Behaviour.PipelineSteps.Skills
{
    internal class ShowSkillsOfUnitStep: IActionStep
    {
        
        public bool Execute(ActionContext actionContext, TableContext tableContext) 
        {
            if (actionContext == null || tableContext == null) return false;

            actionContext.owner.skillContainer.ShowSkills();
            return true;
        }
    }
}
