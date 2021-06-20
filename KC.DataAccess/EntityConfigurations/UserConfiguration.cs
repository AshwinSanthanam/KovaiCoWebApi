using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        protected override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(20);

            builder.Property(x => x.RoleId).IsRequired();

            builder.Property(x => x.IsSocialLogin).HasColumnType("bit").IsRequired();

            builder.HasIndex(x => new { x.Email, x.Password });
        }
    }
}
