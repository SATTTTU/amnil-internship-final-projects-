using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.Ecommerce.Orders.Dtos;
using Acme.Ecommerce.Domain.Entities;

namespace Acme.Ecommerce.Orders
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
            ValidateInput(input);

            var order = new Order(GuidGenerator.Create(), input.CustomerId);

            foreach (var itemDto in input.OrderItems)
            {
                var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == itemDto.ProductId);

                if (product == null)
                {
                    throw new BusinessException("ProductNotFound")
                        .WithData("ProductId", itemDto.ProductId);
                }

                if (itemDto.Quantity <= 0)
                {
                    throw new BusinessException("InvalidQuantity")
                        .WithData("Quantity", itemDto.Quantity);
                }

                // Add domain logic
                order.AddOrderItem(
                    itemDto.ProductId,
                    itemDto.Quantity,
                    product.Price
                );
            }

            await _orderRepository.InsertAsync(order, autoSave: true);

            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        private void ValidateInput(CreateOrderDto input)
        {
            if (input == null)
            {
                throw new BusinessException("InputNull")
                    .WithData("Message", "Order input cannot be null.");
            }

            if (input.CustomerId == Guid.Empty)
            {
                throw new BusinessException("InvalidCustomer")
                    .WithData("Message", "CustomerId is invalid.");
            }

            if (input.OrderItems == null || input.OrderItems.Count == 0)
            {
                throw new BusinessException("OrderItemsMissing")
                    .WithData("Message", "Order must contain at least one item.");
            }
        }
    }
}
