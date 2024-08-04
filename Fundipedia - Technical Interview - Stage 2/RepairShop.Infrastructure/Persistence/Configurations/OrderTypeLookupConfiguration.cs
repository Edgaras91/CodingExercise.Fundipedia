using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepairShop.Infrastructure.Entities;

namespace RepairShop.Infrastructure.Persistence.Configurations
{
    public class OrderTypeLookupConfiguration : IEntityTypeConfiguration<OrderTypeLookup>
    {
        public void Configure(EntityTypeBuilder<OrderTypeLookup> builder)
        {
            builder.ToTable("OrderTypeLookup", "dbo");

            builder.HasKey(x => x.OrderTypeId);
            builder.Property(x => x.OrderTypeId).HasColumnName("OrderTypeID").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.OrderTypeName).HasColumnName("OrderTypeName").HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.OrderStatuses).WithOne(x => x.OrderTypeLookup)
                .HasForeignKey(x => x.OrderStatusLookupId);
        }
    }
}