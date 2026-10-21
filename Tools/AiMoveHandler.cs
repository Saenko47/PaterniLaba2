using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class AiMoveHandler: IMoveHandler
    {
        private BaseUnit _owner;
        private IMove _move;

        public AiMoveHandler(BaseUnit owner,IMove move) 
        {
            _owner = owner;
            _move = move;
        }

        private void MoveRandomly(Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            if (_owner == null || _move == null) return;

            Random random = new Random();
            int randomDirection = random.Next(0, 4);

            switch (randomDirection)
            {
                case 0:
                    _move.MoveUp(_owner, bounds, isCellFree, onMove);
                    break;
                case 1:
                    _move.MoveDown(_owner, bounds, isCellFree, onMove);
                    break;
                case 2:
                    _move.MoveLeft(_owner, bounds, isCellFree, onMove);
                    break;
                case 3:
                    _move.MoveRight(_owner, bounds, isCellFree, onMove);
                    break;
            }
        }

        public void Move(Vector2Int bounds,Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove) 
        {
            MoveRandomly(bounds, isCellFree, onMove);
        }
    }
}
