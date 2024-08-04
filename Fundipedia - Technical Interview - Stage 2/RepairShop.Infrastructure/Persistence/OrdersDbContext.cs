using Microsoft.EntityFrameworkCore;
using RepairShop.Infrastructure.Entities;
using RepairShop.Infrastructure.Persistence.Configurations;
using RepairShop.Infrastructure.Persistence.Interfaces;

namespace RepairShop.Infrastructure.Persistence
{
    public class OrdersDbContext : DbContext, IOrdersDbContext
    {
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderTypeLookup> OrderTypeLookups { get; set; }
        public DbSet<OrderStatusLookup> OrderStatusLookups { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderTypeLookupConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusLookupConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}