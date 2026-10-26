using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces
{
    internal interface IMove
    {
        bool MoveUp(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove);
        bool MoveLeft(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove);
        bool MoveRight(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove);
        bool MoveDown(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove);
    }
}
