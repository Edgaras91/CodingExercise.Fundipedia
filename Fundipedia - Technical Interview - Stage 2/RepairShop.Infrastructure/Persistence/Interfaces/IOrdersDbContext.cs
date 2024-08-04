using Microsoft.EntityFrameworkCore;
using RepairShop.Infrastructure.Entities;

namespace RepairShop.Infrastructure.Persistence.Interfaces
{
    public interface IOrdersDbContext
    {
        DbSet<OrderStatus> OrderStatuses { get; set; }
        DbSet<OrderTypeLookup> OrderTypeLookups { get; set; }
        DbSet<OrderStatusLookup> OrderStatusLookups { get; set; }
    }
}