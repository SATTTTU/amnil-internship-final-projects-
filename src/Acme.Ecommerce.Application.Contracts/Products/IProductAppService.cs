using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Products.Dtos;

namespace Acme.Ecommerce.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<ProductDto> CreateAsync(CreateUpdateProductDto input);
        Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
        Task DeleteAsync(Guid id);
    }
}
