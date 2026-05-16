using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.ItemOrdered, io =>
            {
                io.Property(io => io.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
