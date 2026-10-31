using PaterniLab2.Models.Base.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.Models
{
    internal class UnitOnBoard
    {
        public BaseUnit unit { get; set; } = null!;
        public bool canMove { get; set; } = true;
       
    }
}
