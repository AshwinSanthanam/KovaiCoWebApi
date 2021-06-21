using KC.Base.Models;
using KC.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace KC.DataAccess.Repository
{
    public class KovaiCoDbContext : DbContext
    {
        public KovaiCoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
        }
    }
}
