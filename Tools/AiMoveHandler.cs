using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class AiMoveHandler: IMoveHandler
    {
        private IMove _move;

        public AiMoveHandler(IMove move) 
        {
            _move = move;
        }

        private bool MoveRandomly(BaseUnit _owner, Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (_owner == null || _move == null) return false;

            Random random = new Random();
            int randomDirection = random.Next(0, 4);

            switch (randomDirection)
            {
                case 0:
                   return _move.MoveUp(_owner, bounds, isCellFree, onMove);
                    
                case 1:
                    return _move.MoveDown(_owner, bounds, isCellFree, onMove);
                   
                case 2:
                    return _move.MoveLeft(_owner, bounds, isCellFree, onMove);
                    
                case 3:
                    return _move.MoveRight(_owner, bounds, isCellFree, onMove);
                default:
                    return false;
            }
        }

        public bool Move(BaseUnit _owner, Vector2Int bounds,Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove) 
        {
            return MoveRandomly(_owner,bounds, isCellFree, onMove);
        }
    }
}
