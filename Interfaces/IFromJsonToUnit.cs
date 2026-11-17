using PaterniLab2.DTO;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IFromJsonToUnit
    {
        BaseUnit ConvertFromJsonToUnit(UnitDto unitDto);
    }
}
