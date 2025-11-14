using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Products.Dtos;

namespace Acme.Ecommerce.Products
{
    public interface IProductAppService : ICrudAppService<
        ProductDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto>
    {
    }
}
