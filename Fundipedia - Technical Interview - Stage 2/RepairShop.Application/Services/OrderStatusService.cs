using RepairShop.Application.Repositories;
using RepairShop.Application.Services.Interfaces;
using OrderStatus = RepairShop.Domain.Models.OrderStatus;

namespace RepairShop.Application.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderStatusService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderStatus> GetOrderStatusAsync(bool isRushOrder, string? orderType, bool isNewCustomer,
            bool isLargeOrder)
        {
            return await _orderRepository.GetOrderStatusAsync(isRushOrder, orderType, isNewCustomer, isLargeOrder);
        }
    }
}