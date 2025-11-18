using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Products;
using Acme.Ecommerce.Products.Dtos;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Acme.Ecommerce.Products
{
    public class CategoryAppService : ICategoryAppService
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

        // ----------------------
        // GET SINGLE
        // ----------------------
        public async Task<CategoryDto> GetAsync(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(id);
                return MapToDto(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Category with ID {CategoryId}", id);
                throw new BusinessException("Could not fetch category.");
            }
        }

        // ----------------------
        // GET LIST WITH PAGINATION
        // ----------------------
        public async Task<List<CategoryDto>> GetListAsync(int page, int pageSize)
        {
            try
            {
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 10;

                var queryable = await _categoryRepository.GetQueryableAsync();

                var categories = queryable
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return categories.Select(MapToDto).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching category list.");
                throw new BusinessException("Could not fetch category list.");
            }
        }

        // ----------------------
        // CREATE
        // ----------------------
        public async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            try
            {
                Validate(input);

                var newCategory = new Category(Guid.NewGuid(), input.Name);
                newCategory.SetDescription(input.Description);

                var savedEntity = await _categoryRepository.InsertAsync(newCategory);
                return MapToDto(savedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category.");
                throw new BusinessException("Could not create category.");
            }
        }

        // ----------------------
        // UPDATE
        // ----------------------
        public async Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            try
            {
                Validate(input);

                var category = await _categoryRepository.GetAsync(id);

                category.SetName(input.Name);
                category.SetDescription(input.Description);

                var updated = await _categoryRepository.UpdateAsync(category);
                return MapToDto(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Category with ID {CategoryId}", id);
                throw new BusinessException("Could not update category.");
            }
        }

        // ----------------------
        // DELETE
        // ----------------------
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Category with ID {CategoryId}", id);
                throw new BusinessException("Could not delete category.");
            }
        }

        // ----------------------
        // VALIDATION METHOD
        // ----------------------
        private void Validate(CreateUpdateCategoryDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
            {
                throw new BusinessException("Category name cannot be empty.");
            }

            if (input.Name.Length > 100)
            {
                throw new BusinessException("Category name cannot exceed 100 characters.");
            }
        }

        // ----------------------
        // MANUAL MAPPER
        // ----------------------
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
