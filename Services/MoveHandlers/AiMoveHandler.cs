using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Services.MoveHandlers
{
    internal class AiMoveHandler: IMoveHandler
    {
        private IMove _move;

        public AiMoveHandler(IMove move) 
        {
            _move = move;
        }

        public bool Move(BaseUnit _owner, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            var random = new Random();
            if (_owner == null || _move == null) return false;

           
            int startIndex = random.Next(0, 4);

           
            for (int i = 0; i < 4; i++)
            {
                int currentDirection = (startIndex + i) % 4;

                bool success = currentDirection switch
                {
                    0 => _move.MoveUp(_owner, bounds, isCellFree, onMove),
                    1 => _move.MoveDown(_owner, bounds, isCellFree, onMove),
                    2 => _move.MoveLeft(_owner, bounds, isCellFree, onMove),
                    3 => _move.MoveRight(_owner, bounds, isCellFree, onMove),
                    _ => false
                };

               
                if (success)
                {
                    return true;
                }
            }

        
            return false;
        }
    }
}
