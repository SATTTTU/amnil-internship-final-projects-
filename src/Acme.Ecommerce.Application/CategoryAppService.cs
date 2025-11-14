using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.Ecommerce.Products.Dtos;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Products;

namespace Acme.Ecommerce.Products
{
    public class CategoryAppService : CrudAppService<
        Category,
        CategoryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto>,
        ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, Guid> repository)
            : base(repository)
        {
        }
    }
}