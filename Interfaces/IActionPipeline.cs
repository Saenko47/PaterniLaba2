using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IActionPipeline
    {
        bool StartPipeline(ActionContext actionContext, TableContext tableContext);
    }
}
