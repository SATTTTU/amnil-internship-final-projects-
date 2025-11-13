using System;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace MyECommerce.Products
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }

        public virtual Collection<Product> Products { get; private set; }

        private Category()
        {
            Products = new Collection<Product>();
        }

        public Category(Guid id, string name) : base(id)
        {
            SetName(name);
            Products = new Collection<Product>();
        }

        public void SetName(string name)
        {
            // Add validation logic here
            Name = name;
        }
    }
}