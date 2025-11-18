using System;
using System.Threading.Tasks;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Inventory.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization;

using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Ecommerce.Inventory
{
    public class InventoryAppService : ApplicationService, IInventoryAppService
    {
        private readonly IRepository<Domain.Entities.Inventory, Guid> _inventoryRepository;
        private readonly ILogger<InventoryAppService> _logger;

        public InventoryAppService(
            IRepository<Domain.Entities.Inventory, Guid> inventoryRepository,
            ILogger<InventoryAppService> logger)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
        }

        // ---------------------------------------------------
        // GET INVENTORY BY PRODUCT
        // ---------------------------------------------------
        public async Task<InventoryDto> GetByProductIdAsync(Guid productId)
        {
            try
            {
                ValidateProductId(productId);

                var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

                if (inventory == null)
                {
                    throw new BusinessException("Inventory record not found for this product.");
                }

                return MapToDto(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching inventory for Product {ProductId}", productId);
                throw new BusinessException("Could not fetch inventory.");
            }
        }
        public async Task<InventoryDto> CreateAsync(UpdateInventoryDto input)
        {
            try
            {
                ValidateProductId(input.ProductId);
                ValidateQuantity(input.Quantity);

                var inventory = new Domain.Entities.Inventory(
                    Guid.NewGuid(),      // use Guid.NewGuid()
                    input.ProductId,
                    input.Quantity
                );

                inventory = await _inventoryRepository.InsertAsync(inventory);

                return MapToDto(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating inventory for product {ProductId}", input.ProductId);
                throw new BusinessException("Could not create inventory.");
            }
        }



        // ---------------------------------------------------
        // INCREASE STOCK
        // ---------------------------------------------------
        [Authorize]
        public async Task<InventoryDto> IncreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            try
            {
                ValidateQuantity(input.Quantity);
                ValidateProductId(productId);

                var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

                if (inventory == null)
                {
                    throw new BusinessException("Inventory record does not exist for this product.");
                }

                inventory.IncreaseStock(input.Quantity);

                await _inventoryRepository.UpdateAsync(inventory);

                return MapToDto(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error increasing stock for Product {ProductId}", productId);
                throw new BusinessException("Could not increase stock.");
            }
        }

        // ---------------------------------------------------
        // DECREASE STOCK
        // ---------------------------------------------------
        [Authorize]
        public async Task<InventoryDto> DecreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            try
            {
                ValidateQuantity(input.Quantity);
                ValidateProductId(productId);

                var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

                if (inventory == null)
                {
                    throw new BusinessException("Inventory record does not exist for this product.");
                }

                inventory.DecreaseStock(input.Quantity);

                await _inventoryRepository.UpdateAsync(inventory);

                return MapToDto(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decreasing stock for Product {ProductId}", productId);
                throw new BusinessException("Could not decrease stock.");
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var exists = await _inventoryRepository.AnyAsync(x => x.Id == id);

                if (!exists)
                {
                    throw new BusinessException("Inventory not found.");
                }

                await _inventoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting inventory {InventoryId}", id);
                throw new BusinessException("Could not delete inventory.");
            }
        }



        // ---------------------------------------------------
        // VALIDATION METHODS
        // ---------------------------------------------------
        private void ValidateProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("ProductId is required.");
            }
        }

        private void ValidateQuantity(int qty)
        {
            if (qty <= 0)
            {
                throw new BusinessException("Quantity must be greater than zero.");
            }
        }

        // ---------------------------------------------------
        // MANUAL MAPPER
        // ---------------------------------------------------
        private InventoryDto MapToDto(Domain.Entities.Inventory entity)
        {
            return new InventoryDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                StockQuantity = entity.StockQuantity,
                CreationTime = entity.CreationTime,
                LastModificationTime = entity.LastModificationTime
            };
        }
    }
}
