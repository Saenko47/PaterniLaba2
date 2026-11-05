using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.GameManager
{
    internal interface ICombatGenerator
    {
        void InitializeArmies(List<Army> armies);
        void GenerateCombat();
    }
}
