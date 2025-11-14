using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.Ecommerce.Products.Dtos;
using Acme.Ecommerce.Domain.Entities;

namespace Acme.Ecommerce.Products
{
    public class ProductAppService : CrudAppService<
        Product,
        ProductDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProductDto>,
        IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository)
            : base(repository)
        {
        }
    }
}