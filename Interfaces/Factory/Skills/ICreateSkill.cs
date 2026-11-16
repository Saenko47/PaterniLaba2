using PaterniLab2.DTO;
using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Interfaces.Factory.Skills
{
    internal interface ICreateSkill
    {
        BaseSkill CreateSkill(int idOfSkill);

    }
}
