using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Unit
{
    internal interface ISkillContainer
    {
        int amountOfSkills { get; }
        void AddSkill(BaseSkill skill);

        void ShowSkills();

        BaseSkill GetSkillByKey(int key);
        IEnumerable<BaseSkill> GetSkills();
    }
}
