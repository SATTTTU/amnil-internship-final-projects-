using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyECommerce.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }

        public virtual ICollection<OrderItem> OrderItems { get; private set; }

        private Order()
        {
            OrderItems = new Collection<OrderItem>();
        }

        public Order(Guid id, Guid customerId) : base(id)
        {
            CustomerId = customerId;
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
            OrderItems = new Collection<OrderItem>();
        }

        public void AddOrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            // Logic to add an item and update total amount
            var orderItem = new OrderItem(Id, productId, quantity, unitPrice);
            OrderItems.Add(orderItem);
            TotalAmount += quantity * unitPrice;
        }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Completed,
        Cancelled
    }
}