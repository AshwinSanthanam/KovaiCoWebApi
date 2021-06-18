using Microsoft.Extensions.DependencyInjection;

namespace KC.WebApi.Registry
{
    public class CorsRegistry : IRegistry
    {
        private readonly IServiceCollection _services;
        private readonly string _corsPolicyName;

        public CorsRegistry(IServiceCollection services, string corsPolicyName)
        {
            _services = services;
            _corsPolicyName = corsPolicyName;
        }

        public void Register()
        {
            _services.AddCors(options => {
                options.AddPolicy(_corsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
    }
}
