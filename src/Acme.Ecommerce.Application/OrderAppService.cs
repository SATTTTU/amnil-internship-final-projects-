using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MyECommerce.Orders.Dtos;
using MyECommerce.Products;

namespace MyECommerce.Orders
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, Guid> _productRepository;

        public OrderAppService(
            IRepository<Order, Guid> orderRepository,
            IRepository<Product, Guid> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = new Order(GuidGenerator.Create(), input.CustomerId);

            foreach (var itemDto in input.OrderItems)
            {
                var product = await _productRepository.GetAsync(itemDto.ProductId);
                order.AddOrderItem(itemDto.ProductId, itemDto.Quantity, product.Price);
            }

            await _orderRepository.InsertAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}