using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class ItemDto
    {
        public string ItemType { get; set; }
        public string ItemName { get; set; } 
        public Dictionary<string, int> Buffs { get; set; } = new();
    }
}
