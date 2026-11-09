using PaterniLab2.Interfaces;
using PaterniLab2.Interfaces.Unit;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Singltons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models.Base
{
    internal class SkillContainer: ISkillContainer
    {

        private Dictionary<int, BaseSkill> skills { get; set; } = new Dictionary<int, BaseSkill>();

        private int maxSkills;

        public SkillContainer(int maxSkills)
        {
            this.maxSkills = maxSkills;
        }

        public int amountOfSkills => skills.Count;
        public void AddSkill(BaseSkill skill)
        {
            if (skill == null || skills.ContainsValue(skill) || skills.Count >= maxSkills)
                return;

            skills.Add(skills.Count, skill);
        }



        public void ShowSkills()
        {

            foreach (var (number, skill) in skills)
            {
                GlobalInputOutput.Instance.AddTextDataToBuffer($"{number} - {skill}");
            }
        }

        public BaseSkill GetSkillByKey(int key) 
        {
            return skills[key];
        }
       

        public IEnumerable<BaseSkill> GetSkills()
        {
            foreach (var skill in skills) yield return skill.Value;
        }
    }
}
