using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairShop.Infrastructure.Entities;

namespace RepairShop.Infrastructure.Persistence.Configurations
{
    public class OrderStatusLookupConfiguration : IEntityTypeConfiguration<OrderStatusLookup>
    {
        public void Configure(EntityTypeBuilder<OrderStatusLookup> builder)
        {
            builder.ToTable("OrderStatusLookup", "dbo");

            builder.HasKey(x => x.OrderStatusLookupId);
            builder.Property(x => x.OrderStatusLookupId).HasColumnName("OrderStatusLookupID").HasColumnType("tinyint")
                .IsRequired();
            builder.Property(x => x.OrderStatusName).HasColumnName("OrderStatusName").HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.OrderStatuses).WithOne(x => x.OrderStatusLookup)
                .HasForeignKey(x => x.OrderStatusLookupId);
        }
    }
}