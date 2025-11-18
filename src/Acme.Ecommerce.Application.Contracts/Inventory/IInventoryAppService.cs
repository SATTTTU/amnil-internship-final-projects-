using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Inventory.Dtos;

namespace Acme.Ecommerce.Inventory
{
    public interface IInventoryAppService : IApplicationService
    {
        Task<InventoryDto> GetByProductIdAsync(Guid productId);
        Task<InventoryDto> IncreaseStockAsync(Guid productId, UpdateInventoryDto input);
        Task<InventoryDto> DecreaseStockAsync(Guid productId, UpdateInventoryDto input);

        Task<InventoryDto> CreateAsync(UpdateInventoryDto input);
        Task DeleteAsync(Guid id);
    }
}
