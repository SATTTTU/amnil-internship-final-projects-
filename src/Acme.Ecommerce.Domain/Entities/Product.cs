using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public decimal Price { get; protected set; }
        public Guid CategoryId { get; protected set; }
        public Category Category { get; protected set; }

        private Product()
        {
        }
        public Product(Guid id, string name, Guid categoryId, decimal price, string description = null)
            : base(id)
        {
            SetName(name);
            SetCategory(categoryId);
            SetPrice(price);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessException("Product name cannot be empty.");
            }

            if (name.Length > 100)
            {
                throw new BusinessException("Product name cannot exceed 100 characters.");
            }

            Name = name.Trim();
        }

        public void SetDescription(string description)
        {
            if (description != null && description.Length > 500)
            {
                throw new BusinessException("Description cannot exceed 500 characters.");
            }

            Description = description?.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new BusinessException("Price must be greater than zero.");
            }

            Price = price;
        }

        public void SetCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {

                throw new BusinessException("CategoryId is required.");
            }

            CategoryId = categoryId;
        }
    }
}
