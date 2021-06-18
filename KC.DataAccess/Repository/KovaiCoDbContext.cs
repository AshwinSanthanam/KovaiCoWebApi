using KC.Base.Models;
using Microsoft.EntityFrameworkCore;

namespace KC.DataAccess.Repository
{
    public class KovaiCoDbContext : DbContext
    {
        public KovaiCoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
