using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
namespace Acme.Ecommerce.Domain.Entities
{
    public class OrderItem : CreationAuditedEntity<Guid>

    {
        public Guid OrderId { get; protected set; }
        public Guid ProductId { get; protected set; }
        public int Quantity { get; protected set; }
        public decimal UnitPrice { get; protected set; }

        public OrderItem(
            Guid id,
            Guid orderId,
            Guid productId,
            int quantity,
            decimal unitPrice)
            : base(id)
        {
            SetOrder(orderId);
            SetProduct(productId);
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
        }

        // -------------------------
        // Validation Methods
        // -------------------------

        public void SetOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new BusinessException("OrderId is required.");
            }

            OrderId = orderId;
        }

        public void SetProduct(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("ProductId is required.");
            }

            ProductId = productId;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new BusinessException("Quantity must be greater than zero.");
            }

            Quantity = quantity;
        }

        public void SetUnitPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new BusinessException("Unit price must be greater than zero.");
            }

            UnitPrice = price;
        }
    }
}
