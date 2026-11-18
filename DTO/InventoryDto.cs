using System;
using System.Collections.Generic;
using System.Text;

namespace PaterniLab2.DTO
{
    public record InventoryDto(
    List<ItemDto> Items = null,
    int JewelryMaxCount = 0
);
}
