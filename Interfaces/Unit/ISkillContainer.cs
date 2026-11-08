using PaterniLab2.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface ISkillContainer
    {
        void AddSkill(ISkill skill);

        void ShowSkills();
        IEnumerable<ISkill> GetSkills();
    }
}
