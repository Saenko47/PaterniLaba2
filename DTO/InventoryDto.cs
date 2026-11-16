using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class InventoryDto
    {
        public List<ItemDto> items { get; set; } = new List<ItemDto>();
        public int JeweryMaxCount { get; set; }
    }
}
