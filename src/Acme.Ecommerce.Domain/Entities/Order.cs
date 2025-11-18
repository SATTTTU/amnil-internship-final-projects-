using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.Ecommerce.Domain.Shared.Enums;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid CustomerId { get; protected set; }
        public DateTime OrderDate { get; protected set; }
        public OrderStatus Status { get; protected set; }
        public decimal TotalAmount { get; protected set; }

        public virtual ICollection<OrderItem> OrderItems { get; protected set; }

        public Order(Guid id, Guid customerId)
            : base(id)
        {
            SetCustomer(customerId);
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            TotalAmount = 0;
            OrderItems = new Collection<OrderItem>();
        }

        // -------------------------
        // Validation Methods
        // -------------------------

        public void SetCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new BusinessException("CustomerId is required.");
            }

            CustomerId = customerId;
        }

        public void SetStatus(OrderStatus status)
        {
            Status = status;
        }

        // -------------------------
        // Behavior Methods
        // -------------------------

        public void AddOrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("ProductId is required.");
            }

            if (quantity <= 0)
            {
                throw new BusinessException("Quantity must be greater than zero.");
            }

            if (unitPrice <= 0)
            {
                throw new BusinessException("Unit price must be greater than zero.");
            }

            var orderItem = new OrderItem(
                Guid.NewGuid(), // OrderItem Id
                Id,             // OrderId
                productId,
                quantity,
                unitPrice
            );

            OrderItems.Add(orderItem);

            TotalAmount += quantity * unitPrice;
        }

        public void RemoveOrderItem(Guid orderItemId)
        {
            var item = FindOrderItem(orderItemId);

            if (item == null)
            {
                throw new BusinessException("Order item not found.");
            }

            TotalAmount -= item.Quantity * item.UnitPrice;

            OrderItems.Remove(item);
        }

        private OrderItem? FindOrderItem(Guid id)
        {
            // ? LINQ instead of foreach
            return OrderItems.FirstOrDefault(x => x.Id == id);
        }
    }
}
