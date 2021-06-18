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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
