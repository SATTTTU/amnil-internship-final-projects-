using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Inventory.Dtos;
using DomainInventory = Acme.Ecommerce.Domain.Entities.Inventory;

using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.Ecommerce.Inventory
{
    public class InventoryAppService : ApplicationService, IInventoryAppService
    {
        private readonly IRepository<DomainInventory, Guid> _inventoryRepository;

        public InventoryAppService(IRepository<DomainInventory, Guid> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryDto> GetByProductIdAsync(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("Inventory.InvalidProductId");
            }

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
            {
                throw new BusinessException("Inventory.NotFound")
                    .WithData("ProductId", productId);
            }

            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> IncreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            ValidateInput(productId, input);

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
            {
                throw new BusinessException("Inventory.NotFound")
                    .WithData("ProductId", productId);
            }

            inventory.IncreaseStock(input.Quantity);

            await _inventoryRepository.UpdateAsync(inventory);

            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> DecreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            ValidateInput(productId, input);

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);

            if (inventory == null)
            {
                throw new BusinessException("Inventory.NotFound")
                    .WithData("ProductId", productId);
            }

            try
            {
                inventory.DecreaseStock(input.Quantity); 
            }
            catch (BusinessException ex)
            {
                throw new BusinessException("Inventory.NotEnoughStock")
                    .WithData("AvailableStock", inventory.Stock)
                    .WithData("RequestedStock", input.Quantity);
            }

            await _inventoryRepository.UpdateAsync(inventory);

            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }

        private void ValidateInput(Guid productId, UpdateInventoryDto input)
        {
            if (productId == Guid.Empty)
            {
                throw new BusinessException("Inventory.InvalidProductId");
            }

            if (input == null)
            {
                throw new BusinessException("Inventory.InvalidInput");
            }

            if (input.Quantity <= 0)
            {
                throw new BusinessException("Inventory.InvalidQuantity")
                    .WithData("Quantity", input.Quantity);
            }
        }
    }
}
