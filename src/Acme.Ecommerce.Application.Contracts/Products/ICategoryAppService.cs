using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.Ecommerce.Products.Dtos;

namespace Acme.Ecommerce.Products
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> GetAsync(Guid id);
        Task<List<CategoryDto>> GetListAsync(int page, int pageSize);
        Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input);
        Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}
