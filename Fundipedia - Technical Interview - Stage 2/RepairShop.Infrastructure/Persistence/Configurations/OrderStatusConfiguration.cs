using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairShop.Infrastructure.Entities;

namespace RepairShop.Infrastructure.Persistence.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasKey(x => x.OrderStatusId);
            builder.ToTable("OrderStatus", "dbo");

            builder.Property(x => x.OrderStatusId).HasColumnName("OrderStatusID").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.IsRushOrder).HasColumnName("IsRushOrder");
            builder.Property(x => x.IsNewCustomer).HasColumnName("IsNewCustomer");
            builder.Property(x => x.IsLargeOrder).HasColumnName("IsLargeOrder");
            builder.Property(x => x.OrderStatusLookupId).HasColumnName("OrderStatusLookupId").HasColumnType("tinyint")
                .IsRequired();
            builder.Property(x => x.OrderTypeLookupId).HasColumnName("OrderTypeID").HasColumnType("tinyint");
            builder.Property(x => x.Priority).HasColumnName("Priority").HasColumnType("int").IsRequired();

            builder.HasOne(x => x.OrderStatusLookup).WithMany(op => op.OrderStatuses)
                .HasForeignKey(x => x.OrderStatusLookupId);
            builder.HasOne(x => x.OrderTypeLookup).WithMany(op => op.OrderStatuses).IsRequired()
                .HasForeignKey(x => x.OrderTypeLookupId);
        }
    }
}