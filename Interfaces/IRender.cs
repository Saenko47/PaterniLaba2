using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using PaterniLab2.Tools;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IRender
    {
        Table Render(List<Army> armies, List<RequestOnHighlihtUnit> highlightRequests = null);
    }
}
