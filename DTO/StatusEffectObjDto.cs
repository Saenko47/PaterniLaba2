using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class StatusEffectObjDto
    {
        public bool IsFrendly { get; set; }
        public bool IsStacking { get; set; }
        public string StatusEffect { get; set; }
        public int TurnsRemains { get; set; }
        public int Value { get; set; }
    }
}
