using PaterniLab2.Interfaces;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class GlobalInputOutput
    {
        private static readonly GlobalInputOutput _instance = new GlobalInputOutput();
        private GlobalInputOutput() { }
        public static GlobalInputOutput Instance => _instance;

        private IRender _render;
        private List<RequestOnHighlihtUnit> currentHighlight = new List<RequestOnHighlihtUnit>();

        private List<string> textData = new List<string>();

        public void SetRender(IRender render) 
        {
            if (render != null) _render = render;
        }

        public void SetHighlight(List<RequestOnHighlihtUnit> requestOnHighlihtUnits) 
        {
            currentHighlight.Clear();
            if (requestOnHighlihtUnits != null) currentHighlight.AddRange(requestOnHighlihtUnits);
        }

        public void AddTextDataToBuffer(string text) 
        {
            if (string.IsNullOrEmpty(text)) return;
            textData.Add(text);
        }
        private void ShowTextData() 
        {
            if (textData == null || textData.Count == 0) return;
            foreach (var text in textData)
            {
                Console.WriteLine(text);
            }
            textData.Clear();
        }
        private void Render(List<Army> armies = null) 
        {
            if (_render == null) return;
            _render.Render(armies, currentHighlight);
            currentHighlight.Clear();
        }
        public void Execute(List<Army> armies = null) 
        {
            ShowTextData();
            if(armies != null) Render(armies);

        }

        public string Read(List<Army> armies = null,string beforeReadline = null) 
        {
            Execute(armies);
            if (beforeReadline != null) Console.Write(beforeReadline);
            string res = Console.ReadLine();

            return res;
        }

        public ConsoleKeyInfo ReadKey(List<Army> armies = null, string beforeReadline = null)
        {
            Execute(armies);
            if (beforeReadline != null) Console.Write(beforeReadline);

            // true — чтобы символ не дублировался автоматически в консоли
            return Console.ReadKey(true);
        }

    }
}
