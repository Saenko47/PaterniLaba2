using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface ISkillContainer
    {
        void AddSkill(ISkill skill);

        void RemoveSkill(ISkill skill);

        IEnumerable<ISkill> GetSkills();
    }
}
