using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class ActionPipeline:IActionPipeline
    {
        List<IActionStep> _actionSteps;

        public ActionPipeline(List<IActionStep> actionSteps) 
        {
            _actionSteps = actionSteps;
        }

        public bool StartPipeline(ActionContext actionContext, TableContext tableContext) 
        {
           
            bool res = false;

            foreach (var actionStep in _actionSteps) 
            {
                res = actionStep.Execute(actionContext, tableContext);
                if (!res) break;
            }

            return res;
        }

    }
}
