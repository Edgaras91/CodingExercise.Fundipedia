using Microsoft.EntityFrameworkCore;
using RepairShop.Application.Repositories;
using RepairShop.Infrastructure.Persistence.Interfaces;
using OrderStatus = RepairShop.Domain.Models.OrderStatus;

namespace RepairShop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrdersDbContext _ordersDbContext;

        public OrderRepository(IOrdersDbContext ordersDbContext)
        {
            _ordersDbContext = ordersDbContext;
        }

        public async Task<OrderStatus> GetOrderStatusAsync(bool isRushOrder, string? orderType, bool isNewCustomer,
            bool isLargeOrder)
        {
            int? orderTypeLookupId = null;
            if (orderType != null)
            {
                var typeLookup = await _ordersDbContext.OrderTypeLookups
                    .Where(x => x.OrderTypeName == orderType)
                    .FirstAsync();

                orderTypeLookupId = typeLookup.OrderTypeId;
            }

            var allEntities = await _ordersDbContext.OrderStatuses
                .Include(x => x.OrderStatusLookup)
                .OrderBy(x => x.Priority)
                .ToListAsync();

            foreach (var entity in allEntities)
            {
                var match = MatchEntity(entity, isRushOrder, orderTypeLookupId, isNewCustomer, isLargeOrder);
                if (match)
                {
                    return new OrderStatus(entity.OrderStatusLookup.OrderStatusName);
                }
            }

            return new OrderStatus("Confirmed");
        }

        private static bool MatchEntity(Entities.OrderStatus orderStatus, bool isRushOrder,
            int? orderTypeLookupId,
            bool isNewCustomer, bool isLargeOrder)
        {
            if (orderStatus.IsRushOrder != default)
                if (orderStatus.IsRushOrder != isRushOrder)
                    return false;

            if (orderStatus.OrderTypeLookupId != default)
                if (orderStatus.OrderTypeLookupId != orderTypeLookupId)
                    return false;

            if (orderStatus.IsNewCustomer != default)
                if (orderStatus.IsNewCustomer != isNewCustomer)
                    return false;

            if (orderStatus.IsLargeOrder != default)
                if (orderStatus.IsLargeOrder != isLargeOrder)
                    return false;

            return true;
        }
    }
}