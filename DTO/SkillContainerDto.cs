using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public record SkillContainerDto(int MaxSkills, List<SkillDto> skills);
}
