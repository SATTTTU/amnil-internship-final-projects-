using System;
using Volo.Abp.Application.Dtos;

namespace MyECommerce.Products.Dtos
{
    public class CategoryDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}