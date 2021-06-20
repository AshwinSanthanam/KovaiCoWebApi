using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(20, 2).IsRequired();

            builder.Property(x => x.ImageUrl).HasColumnType("varchar").HasMaxLength(500);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasIndex(x => x.Price);
        }
    }
}
