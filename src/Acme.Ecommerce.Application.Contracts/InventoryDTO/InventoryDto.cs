using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Ecommerce.Inventory.Dtos
{
    public class InventoryDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        public int StockQuantity { get; set; }
    }
}