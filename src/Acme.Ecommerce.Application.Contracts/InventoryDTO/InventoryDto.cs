using System;
using Volo.Abp.Application.Dtos;

namespace MyECommerce.Inventory.Dtos
{
    public class InventoryDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
    }
}