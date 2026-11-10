using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;

using PaterniLab2.Requests.Units;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Tools.PipelineSteps.Skills
{
    internal class ChooseTargetCast: IActionStep
    {
        private void ShowTargets(List<BaseUnit> units) 
        {
            var reqsToHigligth = new List<RequestOnHighlihtUnit>();
            for (int k = 0; k < units.Count; k++)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer($"{k + 1} - unit on coordinates:{units[k].Position}");
                reqsToHigligth.Add(new RequestOnHighlihtUnit { highlightType = HighlightType.CastSkillOnUnit, position = units[k].Position });

            }

            GlobalInputOutput.Instance.SetHighlight(reqsToHigligth);
        }
        private List<BaseUnit> FindUnits(ActionContext actionContext, TableContext tableContext)
        {
            if (actionContext == null
                 || actionContext.parametrsToFindTarget == null
                 || actionContext.targetFinder == null
                 || actionContext.parametrsToFindTarget == null) throw new Exception("Problem With finding units!!!");
           
            var unitsToCast = actionContext.targetFinder.Invoke(actionContext.parametrsToFindTarget);

            

            return unitsToCast;
        }

        private bool ChooseTarget(ActionContext actionContext, TableContext tableContext, List<BaseUnit> units)
        {
            var decision = GlobalInputOutput.Instance.Read(tableContext.armies, "Choose enemy to cast:");
            if (!int.TryParse(decision, out int selectedTargetIndex)
                || selectedTargetIndex < 1 || selectedTargetIndex > units.Count)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer("Action Failed!!!");
                return false;
            }

            actionContext.target = new List<BaseUnit>() { units[selectedTargetIndex-1] };
            return true;

        }
        public bool Execute(ActionContext actionContext, TableContext tableContext) 
        {
            var units = FindUnits(actionContext, tableContext);
            ShowTargets(units);
            return ChooseTarget( actionContext,  tableContext, units);
        }
    }
}
