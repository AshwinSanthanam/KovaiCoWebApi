using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("bigint").UseIdentityColumn();

            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(20).IsRequired();

            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();

            builder.Property(x => x.CreatedOn).HasColumnType("datetimeoffset").IsRequired();

            builder.Property(x => x.UpdatedOn).HasColumnType("datetimeoffset");

            builder.HasIndex(x => new { x.Email, x.Password });
        }
    }
}
