using ApplicationCore.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var navigate = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigate?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(o => o.BuyerId)
                .IsRequired()
                .HasMaxLength(256);

            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(180);
                a.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);
                a.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(60);
                a.Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(90);
                a.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(18);

            });
            // 配置地址非空
            builder.Navigation(x => x.ShipToAddress).IsRequired();
        }
    }
}
