using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Interfaces
{
    internal interface IFSNState
    {
        ResultOfAction Enter(TableContext context, ActionContext actionContext);
    }
}
