using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    public class RoleConfiguration : BaseConfiguration<Role>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
