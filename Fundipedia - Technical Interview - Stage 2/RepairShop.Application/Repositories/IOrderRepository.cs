using OrderStatus = RepairShop.Domain.Models.OrderStatus;

namespace RepairShop.Application.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderStatus> GetOrderStatusAsync(bool isRushOrder, string? orderType, bool isNewCustomer,
            bool isLargeOrder);
    }
}