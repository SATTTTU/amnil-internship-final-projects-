using System;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public virtual Collection<Product> Products { get; protected set; }

        private Category()
        {
            Products = new Collection<Product>();
        }

        public Category(Guid id, string name, string description = null)
            : base(id)
        {
            Products = new Collection<Product>();

            SetName(name);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessException("Category name cannot be empty.");
            }

            if (name.Length > 100)
            {
                throw new BusinessException("Category name cannot exceed 100 characters.");
            }

            Name = name.Trim();
        }

        public void SetDescription(string description)
        {
            if (description != null && description.Length > 500)
            {
                throw new BusinessException("Category description cannot exceed 500 characters.");
            }

            Description = description?.Trim();
        }
    }
}
