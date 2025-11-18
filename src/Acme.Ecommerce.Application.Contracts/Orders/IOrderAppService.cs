using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Orders.Dtos;
using Acme.Ecommerce.Domain.Shared.Enums;

namespace Acme.Ecommerce.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto input);

        Task<OrderDto> GetAsync(Guid id);

        Task<PagedResultDto<OrderDto>> GetListAsync(PagedResultRequestDto input);

        Task<OrderItemDto> CreateOrderItemAsync(Guid orderId, CreateOrderItemDto input);

        Task<bool> RemoveOrderItemAsync(Guid orderId, Guid orderItemId);

        Task<OrderDto> UpdateStatusAsync(Guid id, OrderStatus status);

        Task<bool> DeleteAsync(Guid id);
    }
}
