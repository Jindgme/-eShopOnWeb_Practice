using ApplicationCore.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            //配置EFCore对Items的导航属性使用字段访问模式
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);


            builder.Property(b => b.BuyerId)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
