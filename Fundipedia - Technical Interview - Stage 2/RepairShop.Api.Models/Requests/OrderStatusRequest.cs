namespace RepairShop.Api.Models.Requests
{
    public record OrderStatusRequest(bool IsRushOrder, string? OrderType, bool IsNewCustomer, bool IsLargeOrder);
}