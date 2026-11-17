using PaterniLab2.DTO;
using PaterniLab2.Interfaces.Factory.Skills;
using PaterniLab2.Models.Base.Unit;
using PaterniLab2.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Factories.Skills
{
    internal class CreateArmorSkill:ICreateSkill
    {
        public BaseSkill CreateSkill(int idOfSkill) 
        {
            switch (idOfSkill) 
            {
                case 1: return new Guard();
                default: throw new Exception("Theres no such skill!!!");
            }
        } 
    }
}
