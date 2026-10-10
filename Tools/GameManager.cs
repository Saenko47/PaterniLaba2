using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class GameManager
    {
        private readonly Table _table;
        private readonly IMove _move;
        //private readonly IAttck _attack

        public GameManager(Table table, IMove move)
        {
            _table = table;
            _move = move;
        }

        public void StartGame()
        {
            // Implement game start logic here
            Console.WriteLine("Game started!");
        }

        private void EndGame()
        {
            // Implement game end logic here
            Console.WriteLine("Game ended!");
        }

        private void ProccesCurrentMove(int x, int y) 
        {
            if (x < 0 || x > _table.xLen) return;
            if (y < 0 || y > _table.yLen) return;
        }
    }
}
