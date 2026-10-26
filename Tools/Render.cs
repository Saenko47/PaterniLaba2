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
        private const string MeleeIcon = "⚔";
        private const string RangedIcon = "↗"; 

        private static ConsoleColor RaceColor(Enums.Race race) => race switch
        {
            Enums.Race.Human => ConsoleColor.Cyan,
            Enums.Race.Elf => ConsoleColor.Green,
            Enums.Race.Orc => ConsoleColor.Red,
            Enums.Race.Dwarf => ConsoleColor.DarkYellow,
            _ => ConsoleColor.White
        };

        private static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public void RenderTable(ITable table, List<BaseUnit> units, int? id = null)
        {
            if (table == null)
            {
                Console.WriteLine("Table or Board is not initialized!");
                return;
            }

            Console.ReadLine();
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var frames = table.GetFrames();
            int xLen = frames.X;
            int yLen = frames.Y;

            // Ширина клетки: лидер(1) + раса(1) + оружие(1) + id
            int maxId = units != null && units.Count > 0 ? units.Max(u => u.id) : 0;
            int idWidth = maxId.ToString().Length;
            int cellWidth = Math.Max(3 + idWidth, yLen.ToString().Length);
            int rowLabelWidth = Math.Max(xLen.ToString().Length, 1);
            string pad = new string(' ', rowLabelWidth + 1);

            string Border(char left, char mid, char right) =>
                pad + left + string.Join(mid.ToString(), Enumerable.Repeat(new string('─', cellWidth), yLen)) + right;

            // ===== Заголовок и легенда =====
            WriteColored("⚔  BATTLEFIELD  ⚔", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("Расы: ");
            WriteColored("[H]uman  ", RaceColor(Enums.Race.Human));
            WriteColored("[E]lf  ", RaceColor(Enums.Race.Elf));
            WriteColored("[O]rc  ", RaceColor(Enums.Race.Orc));
            WriteColored("[D]warf", RaceColor(Enums.Race.Dwarf));
            Console.WriteLine();

            Console.Write("Лидер: ");
            WriteColored("★", ConsoleColor.Yellow);
            Console.WriteLine($"   Оружие: {MeleeIcon} меле, {RangedIcon} дальний бой   Число — ID юнита");
            Console.WriteLine();

            // ===== Номера столбцов =====
            Console.Write(pad + " ");
            for (int y = 0; y < yLen; y++)
            {
                string label = y.ToString().PadLeft((cellWidth + y.ToString().Length) / 2).PadRight(cellWidth);
                WriteColored(label, ConsoleColor.DarkGray);
                Console.Write(" ");
            }
            Console.WriteLine();

            // ===== Сетка =====
            Console.WriteLine(Border('┌', '┬', '┐'));

            for (int x = 0; x < xLen; x++)
            {
                WriteColored(x.ToString().PadLeft(rowLabelWidth) + " ", ConsoleColor.DarkGray);
                Console.Write("│");

                for (int y = 0; y < yLen; y++)
                {
                    var unit = units?.FirstOrDefault(u => u.x == x && u.y == y);

                    if (unit == null)
                    {
                        int left = (cellWidth - 1) / 2;
                        string empty = new string(' ', left) + "·" + new string(' ', cellWidth - left - 1);
                        WriteColored(empty, ConsoleColor.DarkGray);
                    }
                    else
                    {
                        // Проверяем: передан ли id И совпадает ли он с текущим юнитом
                        bool isSelected = id.HasValue && (unit.id == id.Value);

                        var originalBackground = Console.BackgroundColor;
                        if (isSelected)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan; // Подсветка выбранного юнита
                        }

                        var color = RaceColor(unit.race);
                        char raceChar = unit.race.ToString()[0];
                        string weapon = unit.weaponType == Enums.WeaponType.Melee ? MeleeIcon : RangedIcon;

                        if (unit.isLeader)
                            WriteColored("★", ConsoleColor.Yellow);
                        else
                            Console.Write(" ");

                        WriteColored(raceChar.ToString(), color);
                        WriteColored(weapon, ConsoleColor.Gray);

                        int printedLength = 1 + 1 + weapon.Length;
                        string idStr = unit.id.ToString().PadRight(cellWidth - printedLength);

                        WriteColored(idStr, color);

                        if (isSelected)
                        {
                            Console.BackgroundColor = originalBackground; // Возвращаем фон назад
                        }
                    }

                    Console.Write("│");
                }

                Console.WriteLine();
                Console.WriteLine(x < xLen - 1 ? Border('├', '┼', '┤') : Border('└', '┴', '┘'));
            }
        }

    }
}
