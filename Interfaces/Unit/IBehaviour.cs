using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface IBehaviour
    {
        bool MakeMove(TableContext context, BaseUnit _owner);
    }
}
