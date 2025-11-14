using System;
using Volo.Abp.Domain.Entities;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Inventory : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int StockQuantity { get; private set; }

        private Inventory() { }

        public Inventory(Guid productId, int initialStock)
        {
            ProductId = productId;
            StockQuantity = initialStock;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity must be positive.");
            StockQuantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity must be positive.");
            if (StockQuantity < quantity) throw new InvalidOperationException("Not enough stock.");
            StockQuantity -= quantity;
        }
    }
}