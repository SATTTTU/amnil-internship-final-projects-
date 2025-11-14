using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Acme.Ecommerce.Domain.Shared.Enums;

using Acme.Ecommerce.Domain.Entities; 

namespace Acme.Ecommerce.Orders.Dtos
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
