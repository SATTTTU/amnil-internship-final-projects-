using System;
using System.Collections.Generic;

namespace Acme.Ecommerce.Orders.Dtos
{
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
