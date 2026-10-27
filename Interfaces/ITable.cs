using PaterniLab2.Models;
using PaterniLab2.Requests.Table;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface ITable
    {

        bool IsSellFree(int x, int y);
        void MoveUnit(Vector2Int from, Vector2Int to);
        Vector2Int GetFrames();
        void ClearCell(int x, int y);
        UnitOnTheBoardByRace FirstPlacementOfUnits(CreateGameRequest req);
      

    }
}
