using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class Render: IRender
    {
        public void RenderTable(Table table, List<BaseUnit> units)
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
                    // Ищем юнита, у которого координаты совпадают с текущей клеткой (x, y)
                    var unit = units?.FirstOrDefault(u => u.x == x && u.y == y);

                    if (unit == null)
                    {
                        Console.Write(" . |");
                    }
                    else
                    {
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

                        // Иконка оружия: Меле или РД
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
