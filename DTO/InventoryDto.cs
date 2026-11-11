using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public class InventoryDto
    {
        public ItemDto Helmet { get; set; }
        public ItemDto Plate { get; set; }
        public ItemDto Leggins { get; set; }
        public ItemDto Boots { get; set; }
        public List<ItemDto> Jewery { get; set; } = new();
        public int JeweryMaxCount { get; set; }
    }
}
