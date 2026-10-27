using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IRender
    {
        public void RenderTable(ITable table, List<BaseUnit> units, Vector2Int? activeCell = null);
    }
}
