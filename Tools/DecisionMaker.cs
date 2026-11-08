using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Tools
{
    internal class DecisionMaker
    {
        public static void ShowVariants(List<string> vartiants) 
        {
            foreach (var variant in vartiants) GlobalInputOutput.Instance.AddTextDataToBuffer(variant);
        }
        public static int MakeDecision(List<string> variants, string textBefore=null) 
        {
            if (textBefore != null) GlobalInputOutput.Instance.AddTextDataToBuffer(textBefore);
            ShowVariants(variants);
            return 0;
        
        }
    }
}
