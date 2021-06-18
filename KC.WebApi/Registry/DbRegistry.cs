using KC.Base;
using KC.Base.Queries;
using KC.Base.Validators;
using KC.DataAccess.Queries;
using KC.DataAccess.Repository;
using KC.DataAccess.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KC.WebApi.Registry
{
    public class DbRegistry : IRegistry
    {
        private readonly IServiceCollection _services;
        private readonly string _connectionString;

        public DbRegistry(IServiceCollection services, string connectionString)
        {
            _services = services;
            _connectionString = connectionString;
        }

        public void Register()
        {
            _services.AddDbContext<KovaiCoDbContext>(o =>
            {
                o.UseSqlServer(_connectionString);
            });

            _services.AddScoped<IRepository, Repository>();
            _services.AddScoped<IUserValidator, UserValidator>();
            _services.AddScoped<IRoleQueries, RoleQueries>();
            _services.AddScoped<IUserQueries, UserQueries>();
        }
    }
}
