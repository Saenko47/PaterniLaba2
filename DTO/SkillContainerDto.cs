using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class SkillContainerDto
    {
        public int MaxSkills { get; set; }
        public List<SkillDto> skills { get; set; } = new List<SkillDto>();
    }
}
