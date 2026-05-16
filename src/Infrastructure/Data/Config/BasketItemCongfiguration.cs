using ApplicationCore.Entities.BasketAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BasketItemCongfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(bi => bi.UnitPrice)
                .IsRequired()
                .HasColumnType(typeName: "decimal(18,2)");  // 显示指定数据库数据类型decimal(18,2)
        }
    }
}
