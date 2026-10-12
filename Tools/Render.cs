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
            Console.ReadLine();
            Console.Clear();

            if (table == null || table._board == null)
            {
                Console.WriteLine("Table or Board is not initialized!");
                return;
            }

            Console.WriteLine("=== BATTLEFIELD ===");
            Console.WriteLine("Расы: [H]uman, [E]lf, [O]rc, [D]warf | Лидер: ★ | Оружие: ⚔ Меле, 🏹 РД");
            Console.WriteLine(new string('-', table.yLen * 4 + 1));

            for (int x = 0; x < table.xLen; x++)
            {
                Console.Write("|");
                for (int y = 0; y < table.yLen; y++)
                {
                    var cell = table._board[x, y];

                    if (cell.placeForUnit == null)
                    {
                        Console.Write(" . |");
                    }
                    else
                    {
                        var unit = cell.placeForUnit;

                        // Выбираем цвет в зависимости от расы
                        Console.ForegroundColor = unit.race switch
                        {
                            Enums.Race.Human => ConsoleColor.Blue,
                            Enums.Race.Elf => ConsoleColor.Green,
                            Enums.Race.Orc => ConsoleColor.Red,
                            Enums.Race.Dwarf => ConsoleColor.DarkYellow,
                            _ => ConsoleColor.White
                        };

                        // Первая буква расы как символ
                        char raceChar = unit.race.ToString()[0];

                        // Если юнит — лидер, помечаем звездочкой, иначе пробел
                        string leaderChar = unit.isLeader ? "★" : " ";

                        // Иконка оружия: Меле или РД (используем string вместо char)
                        string weaponChar = unit.weaponType == Enums.WeaponType.Melee ? "⚔" : "🏹";

                        Console.Write($"{leaderChar}{raceChar}{weaponChar}|");

                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', table.yLen * 4 + 1));
            }
        }
    }
}
