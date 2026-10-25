using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.Requests.Units
{
    internal class RequestOnRecievingStatusEffect
    {
        public bool isFrendly { get; set; }

        public bool isOneTimer { get; set; }

        public StatusEffect statusEffects { get; set; }

        public int duration { get; set; }

        public int value { get; set; }
    }
}
