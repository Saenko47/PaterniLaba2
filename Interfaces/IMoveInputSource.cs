using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IMoveInputSource
    {
        bool ProcessMovement(BaseUnit unit, IMove moveHandler, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove);
    }
}
