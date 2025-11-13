using System;
using Volo.Abp.Application.Dtos;

namespace MyECommerce.Orders.Dtos
{
    public class OrderItemDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}