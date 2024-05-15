using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CatalogBrandEntityConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {

        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");
            builder.Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
