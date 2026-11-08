using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Base
{
    internal abstract class BaseSkill
    {
        
        public abstract bool PerformSkill(BaseUnit owner, List<BaseUnit> targets);
    }
}
