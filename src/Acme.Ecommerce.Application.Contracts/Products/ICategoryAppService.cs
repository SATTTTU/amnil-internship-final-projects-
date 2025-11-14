using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Products.Dtos;

namespace Acme.Ecommerce.Products
{
    public interface ICategoryAppService : ICrudAppService<
        CategoryDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCategoryDto>
    {
    }
}
