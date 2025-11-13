using System;

namespace MyECommerce.Inventory.Dtos
{
    public class UpdateInventoryDto
    {
        public int Quantity { get; set; }
    }
}```

#### Application Layer (`.Application`)

**Inventory Application Service(`InventoryAppService.cs`)**

```csharp
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MyECommerce.Inventory.Dtos;

namespace MyECommerce.Inventory
{
    public class InventoryAppService : ApplicationService, IInventoryAppService
    {
        private readonly IRepository<Inventory, Guid> _inventoryRepository;

        public InventoryAppService(IRepository<Inventory, Guid> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryDto> GetByProductIdAsync(Guid productId)
        {
            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == productId);
            return ObjectMapper.Map<Inventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> IncreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            var inventory = await _inventoryRepository.FirstAsync(i => i.ProductId == productId);
            inventory.IncreaseStock(input.Quantity);
            await _inventoryRepository.UpdateAsync(inventory);
            return ObjectMapper.Map<Inventory, InventoryDto>(inventory);
        }

        public async Task<InventoryDto> DecreaseStockAsync(Guid productId, UpdateInventoryDto input)
        {
            var inventory = await _inventoryRepository.FirstAsync(i => i.ProductId == productId);
            inventory.DecreaseStock(input.Quantity);
            await _inventoryRepository.UpdateAsync(inventory);
            return ObjectMapper.Map<Inventory, InventoryDto>(inventory);
        }
    }
}