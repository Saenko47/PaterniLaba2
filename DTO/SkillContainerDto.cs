using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class SkillContainerDto
    {
        public int MaxSkills { get; set; }
        public Dictionary<int, SkillDto> Skills { get; set; } = new();
    }
}
