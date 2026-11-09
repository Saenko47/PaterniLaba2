using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IActionStep
    {
        bool Execute(ActionContext actionContext, TableContext tableContext);
    }
}
