using ApplicationCore.Entities.CatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.Property(ct => ct.Id)
                .UseHiLo("catalog_type_hilo")
                .IsRequired();

            builder.Property(ct => ct.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
