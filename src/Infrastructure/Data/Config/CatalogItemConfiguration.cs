using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(ci => ci.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ci => ci.PictureUri)
                .IsRequired(false);  // NULL

            // 设置CatalogBrand对CatalogItem，一对多。
            // WithMany()  表示CatalogBrand中没配置ICollection<CatalogItem>属性
            builder.HasOne(ci => ci.CatalogBrand)
                .WithMany()  
                .HasForeignKey(ci => ci.CatalogBrandId);

            builder.HasOne(ci => ci.CatalogType)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);
                
        }
    }
}
