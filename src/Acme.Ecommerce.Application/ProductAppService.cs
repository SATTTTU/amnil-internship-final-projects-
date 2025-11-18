using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Acme.Ecommerce.Products.Dtos;
using Acme.Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization;


namespace Acme.Ecommerce.Products
{
    public class ProductAppService :  IProductAppService
    {
        private readonly IRepository<Product, Guid> _repository;
        private readonly ILogger<ProductAppService> _logger;

        public ProductAppService(
            IRepository<Product, Guid> repository,
            ILogger<ProductAppService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // VALIDATION 
        private void ValidateProductInput(CreateUpdateProductDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new BusinessException("Product name cannot be empty.");
            }

            if (input.Price <= 0)
            {
                throw new BusinessException("Price must be greater than zero.");
            }

            if (input.CategoryId == Guid.Empty)
            {
                throw new BusinessException("CategoryId is required.");
            }
        }

        // MAPPING
        private ProductDto MapToDto(Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                Price = entity.Price,
                CreationTime = entity.CreationTime
            };
        }

        private void MapToEntity(Product entity, CreateUpdateProductDto input)
        {
            entity.SetName(input.Name);
            entity.SetDescription(input.Description);
            entity.SetPrice(input.Price);
            entity.SetCategory(input.CategoryId);
        }

        // CRUD METHODS

        public async Task<ProductDto> GetAsync(Guid id)
        {
            try
            {
                var product = await _repository.GetAsync(id);
                return MapToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product {Id}", id);
                throw;
            }
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                var queryable = await _repository.GetQueryableAsync();

                var totalCount = queryable.Count();

                var items = queryable
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();

                return new PagedResultDto<ProductDto>(
                    totalCount,
                    items.Select(MapToDto).ToList()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product list");
                throw;
            }
        }
        [Authorize]
        public async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            try
            {
                ValidateProductInput(input);

                var product = new Product(
                    Guid.NewGuid(),
                    input.Name,
                    input.CategoryId,
                    input.Price,
                    input.Description
                );

                await _repository.InsertAsync(product, autoSave: true);

                return MapToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }
        [Authorize]
        public async Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            try
            {
                ValidateProductInput(input);

                var product = await _repository.GetAsync(id);

                MapToEntity(product, input);

                await _repository.UpdateAsync(product, autoSave: true);

                return MapToDto(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {Id}", id);
                throw;
            }
        }
        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {Id}", id);
                throw;
            }
        }
    }
}
