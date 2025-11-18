using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Orders;
using Acme.Ecommerce.Orders.Dtos;
using Acme.Ecommerce.Domain.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization;

using System.Collections.Generic;

namespace Acme.Ecommerce.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly ILogger<OrderAppService> _logger;

        public OrderAppService(IRepository<Order, Guid> orderRepository, ILogger<OrderAppService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        // -------------------------------------------------------------------
        // CREATE ORDER
        // -------------------------------------------------------------------
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            try
            {
                ValidateCreateOrder(input);

                var order = new Order(
                    Guid.NewGuid(),
                    input.CustomerId
                );

                await _orderRepository.InsertAsync(order, autoSave: true);

                return MapToOrderDto(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order.");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // GET SINGLE ORDER
        // -------------------------------------------------------------------
        public async Task<OrderDto> GetAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(id);
                return MapToOrderDto(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order.");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // PAGINATED LIST
        // -------------------------------------------------------------------
        public async Task<PagedResultDto<OrderDto>> GetListAsync(PagedResultRequestDto input)
        {
            try
            {
                var queryable = await _orderRepository.GetQueryableAsync();

                var totalCount = queryable.Count();

                var orders = queryable
                    .OrderByDescending(x => x.CreationTime)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();

                var dtos = orders.Select(MapToOrderDto).ToList();

                return new PagedResultDto<OrderDto>(totalCount, dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching order list.");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // CREATE ORDER ITEM
        // -------------------------------------------------------------------
        public async Task<OrderItemDto> CreateOrderItemAsync(Guid orderId, CreateOrderItemDto input)
        {
            try
            {
                ValidateOrderItem(input);

                var order = await _orderRepository.GetAsync(orderId);

                order.AddOrderItem(
                    input.ProductId,
                    input.Quantity,
                    input.UnitPrice
                );

                await _orderRepository.UpdateAsync(order, autoSave: true);

                var createdItem = order.OrderItems.Last();

                return MapToOrderItemDto(createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order item.");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // REMOVE ORDER ITEM
        // -------------------------------------------------------------------
        public async Task<bool> RemoveOrderItemAsync(Guid orderId, Guid orderItemId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(orderId);

                order.RemoveOrderItem(orderItemId);

                await _orderRepository.UpdateAsync(order, autoSave: true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing OrderItem {orderItemId}");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // UPDATE ORDER STATUS
        // -------------------------------------------------------------------
        public async Task<OrderDto> UpdateStatusAsync(Guid id, OrderStatus status)
        {
            try
            {
                var order = await _orderRepository.GetAsync(id);
                order.SetStatus(status);

                await _orderRepository.UpdateAsync(order, autoSave: true);

                return MapToOrderDto(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order status.");
                throw;
            }
        }

        // -------------------------------------------------------------------
        // DELETE ORDER
        // -------------------------------------------------------------------
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order.");
                throw;
            }
        }

        // ================================================================
        // VALIDATION METHODS
        // ================================================================
        private void ValidateCreateOrder(CreateOrderDto input)
        {
            if (input.CustomerId == Guid.Empty)
                throw new BusinessException("CustomerId is required.");
        }

        private void ValidateOrderItem(CreateOrderItemDto input)
        {
            if (input.ProductId == Guid.Empty)
                throw new BusinessException("ProductId is required.");

            if (input.Quantity <= 0)
                throw new BusinessException("Quantity must be greater than zero.");

            if (input.UnitPrice <= 0)
                throw new BusinessException("UnitPrice must be greater than zero.");
        }

        // ================================================================
        // MANUAL MAPPING METHODS
        // ================================================================
        private OrderDto MapToOrderDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(MapToOrderItemDto).ToList()
            };
        }

        private OrderItemDto MapToOrderItemDto(OrderItem item)
        {
            return new OrderItemDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };
        }
    }
}
