using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Requests
{
    internal class ActionContext
    {
        public BaseUnit owner { get; set; } = null!;

        public List<BaseUnit>? target { get; set; } 
    }
}
