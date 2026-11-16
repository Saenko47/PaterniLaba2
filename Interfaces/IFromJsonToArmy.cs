using PaterniLab2.DTO;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IFromJsonToArmy
    {
        List<Army> ConvertFromJsonToArmy(List<ArmyDto> armieDtos);
    }
}
