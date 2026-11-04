using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IFSNState
    {
        bool Enter(TableContext context, ActionContext actionContext);
    }
}
