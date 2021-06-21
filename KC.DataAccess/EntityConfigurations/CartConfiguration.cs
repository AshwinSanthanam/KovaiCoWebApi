using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    class CartConfiguration : BaseConfiguration<Cart>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.Property(x => x.Quantity).IsRequired();

            builder.HasIndex(x => new { x.ProductId, x.UserId });
        }
    }
}
