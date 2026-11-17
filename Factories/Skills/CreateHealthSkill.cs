using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Skills;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Skills
{
    internal class CreateHealthSkill:ICreateSkill
    {
        public BaseSkill CreateSkill(int idOfSkill) 
        {
            switch (idOfSkill) 
            {
                case 1: return new HealSkill();
                case 2: return new FireballSkill();
                default: throw new Exception("Theres no such hea skill!!!");
            }
        }

    }
}
