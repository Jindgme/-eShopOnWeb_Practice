using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.Property(cb => cb.Id)
                .UseHiLo("catalog_brand_hilo")
                .IsRequired();

            builder.Property(cb => cb.Brand)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
