using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class ActionContext
    {
        public BaseUnit owner { get; set; } = null!;

        public List<BaseUnit>? target { get; set; }
        public Func<ReqiestOnFindingUnit, List<BaseUnit>?>? targetFinder { get; set; }
        public Func<BaseUnit, BaseUnit, bool>? attackExecutionStrategy { get; set; }
    }
}
