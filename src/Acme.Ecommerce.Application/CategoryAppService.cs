using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization;

using Acme.Ecommerce.Products.Dtos;
using Microsoft.Extensions.Logging;
using Acme.Ecommerce.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Dtos;
using Volo.Abp;

namespace Acme.Ecommerce.Products
{
    public class CategoryAppService : EcommerceAppService, ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly ILogger<CategoryAppService> _logger;

        public CategoryAppService(
            IRepository<Category, Guid> categoryRepository,
            ILogger<CategoryAppService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        // ---------------------------------------
        // GET BY ID
        // ---------------------------------------
        [AllowAnonymous]
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            try
            {
                var entity = await _categoryRepository.GetAsync(id);
                return MapToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category with ID {Id}", id);
                throw new BusinessException("Could not fetch category.");
            }
        }

        // ---------------------------------------
        // GET LIST WITH PAGINATION
        // ---------------------------------------
        [AllowAnonymous]
        public async Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                var queryable = await _categoryRepository.GetQueryableAsync();

                var totalCount = queryable.Count();

                var items = queryable
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();

                return new PagedResultDto<CategoryDto>(
                    totalCount,
                    items.Select(MapToDto).ToList()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category list.");
                throw new BusinessException("Could not fetch category list.");
            }
        }

        // ---------------------------------------
        // CREATE
        // ---------------------------------------
        [Authorize(EcommercePermissions.Categories.Create)]
        public async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            try
            {
                Validate(input);

                var entity = new Category(Guid.NewGuid(), input.Name);
                entity.SetDescription(input.Description);

                var saved = await _categoryRepository.InsertAsync(entity);
                return MapToDto(saved);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category.");
                throw new BusinessException("Could not create category.");
            }
        }

        // ---------------------------------------
        // UPDATE
        // ---------------------------------------
        [Authorize(EcommercePermissions.Categories.Edit)]
        public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            try
            {
                Validate(input);

                var entity = await _categoryRepository.GetAsync(id);

                entity.SetName(input.Name);
                entity.SetDescription(input.Description);

                var updated = await _categoryRepository.UpdateAsync(entity);
                return MapToDto(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with ID {Id}", id);
                throw new BusinessException("Could not update category.");
            }
        }

        // ---------------------------------------
        // DELETE
        // ---------------------------------------
        [Authorize(EcommercePermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category with ID {Id}", id);
                throw new BusinessException("Could not delete category.");
            }
        }

        // ---------------------------------------
        // VALIDATION (AppService-level)
        // ---------------------------------------
        private void Validate(CreateUpdateCategoryDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new BusinessException("Name is required.");
            }

            if (input.Name.Length > 100)
            {
                throw new BusinessException("Name cannot exceed 100 characters.");
            }
        }

        // MANUAL MAPPER
        private CategoryDto MapToDto(Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
