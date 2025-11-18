using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Inventory : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; protected set; }
        public int StockQuantity { get; protected set; }
        public Product Product { get; protected set; }

        public Inventory(Guid id, Guid productId, int initialStock)
            : base(id)
        {
            SetProduct(productId);
            SetInitialStock(initialStock);
        }

        public void SetProduct(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("ProductId is required.");
            }

            ProductId = productId;
        }

        public void SetInitialStock(int initialStock)
        {
            if (initialStock < 0)
            {
                throw new BusinessException("Initial stock cannot be negative.");
            }

            StockQuantity = initialStock;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new BusinessException("Quantity must be greater than zero.");
            }

            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new BusinessException("Quantity must be greater than zero.");
            }

            if (StockQuantity < quantity)
            {
                throw new BusinessException("Not enough stock to reduce.");
            }

            StockQuantity -= quantity;
        }
    }
}
