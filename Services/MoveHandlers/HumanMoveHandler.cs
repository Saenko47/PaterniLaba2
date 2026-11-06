using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class HumanKeyboardMoveInput: IMoveHandler
    {
     
        private IMove _move;

        public HumanKeyboardMoveInput(IMove move)
        {
            _move = move;
        }
        public bool Move(BaseUnit _owner,Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {

            GlobalInputOutput.Instance.AddTextDataToBuffer("Введите направление (W/A/S/D):");
            string input = GlobalInputOutput.Instance.Read();
            char key = char.ToUpper(Convert.ToChar(input));
          
            // Переводим нажатие клавиши в вызов конкретного метода из IMove
            return key switch
            {
                'W' => _move.MoveUp(_owner, bounds, isCellFree, onMove),
                'S' => _move.MoveDown(_owner, bounds, isCellFree, onMove),
                'A' => _move.MoveLeft(_owner, bounds, isCellFree, onMove),
                'D' => _move.MoveRight(_owner, bounds, isCellFree, onMove),
                _ => false // Неверная клавиша
            };
        }
    }
}
