using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Ecommerce.Domain.Entities
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product()
        {
        }

        public Product(Guid id, string name, Guid categoryId) : base(id)
        {
            SetName(name);
            SetCategory(categoryId);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetCategory(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}