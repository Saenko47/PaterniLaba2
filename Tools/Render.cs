using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Render: IRender
    {
        public void RenderTable(Table table) 
        {
            if (table == null || table._board == null)
            {
                Console.WriteLine("Table or Board is not initialized!");
                return;
            }
            for (int x = 0; x < table.xLen; x++)
            {
                for (int y = 0; y < table.yLen; y++)
                {
                    Console.Write(table._board[x, y].placeForUnit == null ? "-" : "+");
                }
                Console.WriteLine();
            }
        }
    }
}
