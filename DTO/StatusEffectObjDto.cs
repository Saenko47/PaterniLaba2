using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class StatusEffectObjDto
    {
        public bool IsFrendly { get; set; }
        public bool IsStacking { get; set; }
        public StatusEffect StatusEffect { get; set; }
        public int TurnsRemains { get; set; }
        public int Value { get; set; }
    }
}
