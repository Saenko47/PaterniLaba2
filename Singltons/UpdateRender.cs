using PaterniLab2.Interfaces;
using PaterniLab2.Models.Units;
using PaterniLab2.Requests;
using PaterniLab2.Requests.Units;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PaterniLab2.Singltons
{
    internal class UpdateRender
    {
        private static readonly UpdateRender _instance = new UpdateRender();

        private IRender _render;

        private List<RequestOnHighlihtUnit> currentHighlight = new List<RequestOnHighlihtUnit>();

        private UpdateRender() { }

        public static UpdateRender Instance => _instance;

        public void SetRender(IRender render) 
        {
            _render = render;
        }
        public void SetHighlight(List<RequestOnHighlihtUnit> req)
        {
            currentHighlight.Clear();
            if (req != null)
            {
                currentHighlight.AddRange(req);
            }
        }
       

        public void Render(List<Army> armiesToRender, List<RequestOnHighlihtUnit> reqs = null) 
        {
            if (_render == null) return;
            if (reqs != null) SetHighlight(reqs);
            SetHighlight(reqs);
            _render.Render(armiesToRender, currentHighlight);
            Console.WriteLine("Enter...");
            Console.ReadLine();
            Console.Clear();
        }
      


    }
}
