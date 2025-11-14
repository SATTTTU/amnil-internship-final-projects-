
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Inventory.Dtos;
using DomainInventory = Acme.Ecommerce.Domain.Entities.Inventory;

using System;
using System.Threading.Tasks;
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
            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);
            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> IncreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            var inventory = await _inventoryRepository.FirstAsync(i => i.ProductId == productId);
            inventory.IncreaseStock(input.Quantity);
            await _inventoryRepository.UpdateAsync(inventory);
            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> DecreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            var inventory = await _inventoryRepository.FirstAsync(i => i.ProductId == productId);
            inventory.DecreaseStock(input.Quantity);
            await _inventoryRepository.UpdateAsync(inventory);
            return ObjectMapper.Map<DomainInventory, InventoryDto>(inventory);
        }
    }
}