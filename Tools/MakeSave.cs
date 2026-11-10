using PaterniLab2.Interfaces;
using PaterniLab2.Models;
using PaterniLab2.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal static class MakeSave
    {
       

        public static void SaveGame(List<Army> armysToSave, ITable table) 
        {
            GameSnippet gameSnippet = new GameSnippet(armysToSave, table.GetFrames());
            //theres will bell be json
        }

        public static GameSnippet LoadGame() 
        {
            //will read json with snippet if exist
            throw new Exception();
        }
    }
}
