using System;
using System.Collections.Generic;
using System.Text;
using static PaterniLab2.Tools.Enums;

namespace PaterniLab2.DTO
{
    public class SkillDto
    {
        public int _id { get; set; }

        public StatusEffect _statusEffect { get; set; }
        public SkillDto() { }

        public SkillDto(int id, StatusEffect statusEffect) 
        {
            _id = id;
            _statusEffect = statusEffect;
        }
    }
}
