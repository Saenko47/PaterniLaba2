using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Move:IMove
    {
        public bool MoveUp(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (unit == null) return false;

            int targetX = unit.x;
            int targetY = unit.y + 1;

        
            if (targetY >= bounds.Y) return false;

        
            if (!isCellFree(targetX, targetY)) return false; 

            int oldX = unit.x;
            int oldY = unit.y;

            unit.SetPosition(targetX, targetY);

            onMove?.Invoke(new Vector2Int(oldX, oldY), new Vector2Int(targetX, targetY));
            return true;
        }
        public bool MoveDown(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (unit == null) return false;

            int targetX = unit.x;
            int targetY = unit.y - 1;

            // Проверка нижней границы
            if (targetY < 0) return false;

            if (!isCellFree(targetX, targetY)) return false;

            int oldX = unit.x;
            int oldY = unit.y;

            unit.SetPosition(targetX, targetY);

            onMove?.Invoke(new Vector2Int(oldX, oldY), new Vector2Int(targetX, targetY));

            return true;
        }

        public bool MoveLeft(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (unit == null) return false;

            int targetX = unit.x - 1;
            int targetY = unit.y;

            // Проверка левой границы
            if (targetX < 0) return false;

            if (!isCellFree(targetX, targetY)) return false;

            int oldX = unit.x;
            int oldY = unit.y;

            unit.SetPosition(targetX, targetY);

            onMove?.Invoke(new Vector2Int(oldX, oldY), new Vector2Int(targetX, targetY));

            return true;
        }

        public bool MoveRight(BaseUnit unit, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (unit == null) return false;

            int targetX = unit.x + 1;
            int targetY = unit.y;

            // Проверка правой границы (bounds.x хранит xLen)
            if (targetX >= bounds.X) return false;

            if (!isCellFree(targetX, targetY)) return false;

            int oldX = unit.x;
            int oldY = unit.y;

            unit.SetPosition(targetX, targetY);

            onMove?.Invoke(new Vector2Int(oldX, oldY), new Vector2Int(targetX, targetY));

            return true;
        }
    }
}
