using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Services.Behaviour.PipelineSteps.Attack
{
    internal class ShowEnemiesStep:IActionStep
    {
        private bool OnFail() 
        {
            GlobalInputOutput.Instance.AddTextDataToBuffer("You dont have any enemies, Torffin");
            return false;
        }

        private bool OnSucces(List<BaseUnit> targets) 
        {
            Console.WriteLine(targets.Count);
            var reqs = new List<RequestOnHighlihtUnit>();
            for (int k = 0; k < targets.Count; ++k)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer($"[{k+1}] {targets[k].race} on {targets[k].Position}");
                var req = new RequestOnHighlihtUnit
                {
                    position = targets[k].Position,
                    highlightType = HighlightType.AttackableUnit
                };
                reqs.Add(req);
            }

            GlobalInputOutput.Instance.SetHighlight(reqs);
            return true;
        } 
        public bool Execute(ActionContext actionContext, TableContext tableContext) 
        {
            if (actionContext == null || tableContext == null) return OnFail();

            var targets = actionContext.targetFinder?.Invoke(new ReqiestOnFindingUnit
            {
                strategysToFind = actionContext.owner.weapon.strategysToFind,
                armies = tableContext.armies,
                isLookingForComrades = false,
                rangeOfFindings = actionContext.owner.weapon.range,
                unitThatLooking = actionContext.owner
            }) ?? new List<BaseUnit>();

            GlobalInputOutput.Instance.AddTextDataToBuffer($"{targets.Count}");

            if (targets == null || targets.Count == 0)
            {
                return OnFail();
            }

            actionContext.target = targets;

            return OnSucces(targets);
          
        }
    } 
    
}
