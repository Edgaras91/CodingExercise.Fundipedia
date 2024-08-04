using RepairShop.Domain.Models;

namespace RepairShop.Application.Services.Interfaces;

public interface IOrderStatusService
{
    Task<OrderStatus> GetOrderStatusAsync(bool isRushOrder, string? orderType, bool isNewCustomer, bool isLargeOrder);
}