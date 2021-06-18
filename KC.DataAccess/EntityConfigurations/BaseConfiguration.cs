using KC.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KC.DataAccess.EntityConfigurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureChild(builder);

            ConfigureParent(builder);
        }

        private static void ConfigureParent(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("bigint").UseIdentityColumn();

            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();

            builder.Property(x => x.CreatedOn).HasColumnType("datetimeoffset").IsRequired();

            builder.Property(x => x.UpdatedOn).HasColumnType("datetimeoffset");
        }

        protected abstract void ConfigureChild(EntityTypeBuilder<T> builder);
    }
}
