using PaterniLab2.Interfaces;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class HumanKeyboardMoveInput: IMoveHandler
    {
        private BaseUnit _owner;
        private IMove _move;

        public HumanKeyboardMoveInput(BaseUnit owner, IMove move)
        {
            _owner = owner;
            _move = move;
        }
        public bool Move(Vector2Int bounds, Func<int, int, bool> isCellFree, Action<Vector2Int, Vector2Int> onMove)
        {
            Console.WriteLine("Введите направление (W/A/S/D):");
            char key = char.ToUpper(Console.ReadKey(true).KeyChar);

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
