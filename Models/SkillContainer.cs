using PaterniLab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal class SkillContainer: ISkillContainer
    {

        public List<ISkill> skills { get; private set; } = new List<ISkill>();
        private int maxSkills;

        public SkillContainer(int maxSkills) 
        {
            this.maxSkills = maxSkills;
        }

        public void AddSkill(ISkill skill) 
        {
            if (skill == null || skills.Contains(skill) || skills.Count > maxSkills) return;

            skills.Add(skill);
        }

        public void RemoveSkill(ISkill skill) 
        {
            if (skill == null) return;

            skills.Remove(skill);
        }

        public IEnumerable<ISkill> GetSkills() 
        {
            foreach (var skill in skills) yield return skill;
        }
    }
}
