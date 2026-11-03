using System;
using System.Collections.Generic;
using System.Text;
using PaterniLab2.Interfaces;
using PaterniLab2.Requests;
using Spectre.Console;

namespace PaterniLab2.Services.Render
{
    using PaterniLab2.Models.Units;
    using Spectre.Console;
    using static PaterniLab2.Tools.Enums;

    internal class RenderTable:IRender
    {
        private readonly ITable _table;

        public RenderTable(ITable table)
        {
            _table = table;
        }

        

        private string GetBackgroundMarkup(HighlightType type)
        {
            switch (type)
            {
                case HighlightType.ActiveUnit:
                    return "on white";     
                case HighlightType.TargetUnit:
                    return "on olive";  
                case HighlightType.AttackableUnit:
                    return "on red";     
                default:
                    return "on grey";
            }
        }

        private string[,] CreateGridBuffer(List<Army> armies, List<RequestOnHighlihtUnit> highlightRequests = null)
        {
            var tableSize = _table.GetFrames();
            string[,] grid = new string[tableSize.Y, tableSize.X];

         
            for (int y = 0; y < tableSize.Y; y++)
            {
                for (int x = 0; x < tableSize.X; x++)
                {
                    grid[y, x] = "[grey]·[/]";
                }
            }

         
            foreach (var army in armies)
            {
                foreach (var unit in army._units)
                {
                    if (unit.x >= 0 && unit.x < tableSize.X && unit.y >= 0 && unit.y < tableSize.Y)
                    {
                        grid[unit.y, unit.x] = $"[{unit.color}]@[/]";
                    }
                }
            }

  
            if (highlightRequests != null)
            {
                foreach (var req in highlightRequests)
                {
                    if (req.position.X >= 0 && req.position.X < tableSize.X && req.position.Y >= 0 && req.position.Y < tableSize.Y)
                    {
                        string bg = GetBackgroundMarkup(req.highlightType);
                        string currentContent = grid[req.position.Y, req.position.X];

                      
                        if (currentContent.Contains("·"))
                        {
                            grid[req.position.Y, req.position.X] = $"[grey {bg}]·[/]";
                        }
                     
                        else
                        {
                         
                            string unitColor = currentContent.Split('[', ']')[1];
                            grid[req.position.Y, req.position.X] = $"[{unitColor} {bg}]@[/]";
                        }
                    }
                }
            }

            return grid;
        }

     
        private Table BuildTable(string[,] grid)
        {
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);

            var table = new Table();
            table.Border(TableBorder.Square);
            table.ShowRowSeparators();

         
            table.AddColumn(new TableColumn("X/Y"));
            for (int x = 0; x < width; x++)
            {
                table.AddColumn(new TableColumn($"[grey]{x}[/]").Centered());
            }

    
            for (int y = 0; y < height; y++)
            {
                var rowValues = new string[width + 1];
                rowValues[0] = $"[grey]{y}[/]"; 

                for (int x = 0; x < width; x++)
                {
                    rowValues[x + 1] = grid[y, x];
                }

                table.AddRow(rowValues);
            }

            return table;
        }

    
        public void Render(List<Army> armies, List<RequestOnHighlihtUnit> highlightRequests = null)
        {

            var buffer = CreateGridBuffer(armies, highlightRequests);
            var table = BuildTable(buffer);
          
            AnsiConsole.Write(table);
            
            
        }
    }
}
