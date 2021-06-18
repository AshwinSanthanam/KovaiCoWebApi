using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace KC.WebApi.Registry
{
    public class SwaggerRegistry : IRegistry
    {
        private readonly IServiceCollection _services;

        public SwaggerRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Register()
        {
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KC.WebApi", Version = "v1" });
            });
        }
    }
}
