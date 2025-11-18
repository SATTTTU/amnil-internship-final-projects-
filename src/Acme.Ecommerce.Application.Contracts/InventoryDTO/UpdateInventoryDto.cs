using System;

namespace Acme.Ecommerce.Inventory.Dtos
{
    public class UpdateInventoryDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

