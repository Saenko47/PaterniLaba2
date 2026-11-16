using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface IChooseAsALeader
    {
        void Elect(BaseUnit unitToElect);
    }
}
