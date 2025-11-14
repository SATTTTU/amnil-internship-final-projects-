using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Acme.Ecommerce.Orders.Dtos;

namespace Acme.Ecommerce.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> CreateAsync(CreateOrderDto input);
    }
}
