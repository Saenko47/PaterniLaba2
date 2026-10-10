using PaterniLab2.Models.Units;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Factory
{
    internal interface ICreateOrc
    {
        OrcUnit CreateOrcUnit(CreateUnitRequest request);
    }
}
