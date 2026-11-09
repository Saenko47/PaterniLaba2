using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface ISkillContainer
    {
        int amountOfSkills { get; }
        void AddSkill(ISkill skill);

        void ShowSkills();

        ISkill GetSkillByKey(int key);
        IEnumerable<ISkill> GetSkills();
    }
}
