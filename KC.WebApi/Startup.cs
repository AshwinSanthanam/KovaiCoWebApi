using KC.WebApi.Registry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace KC.WebApi
{
    public class Startup
    {
        private static readonly string _corsPolicyName = "Open";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            IEnumerable<IRegistry> registries = new IRegistry[]
            {
                new JwtRegistry(services, Configuration.GetSection("Jwt:Key").Value),
                new CorsRegistry(services, _corsPolicyName),
                new DbRegistry(services, Configuration.GetConnectionString("KovaiCoRepository")),
                new SwaggerRegistry(services),
                new ServiceRegistry(services),
                new AutoMapperRegistry(services)
            };

            RegisterRegistry(registries);

            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KC.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterRegistry(IEnumerable<IRegistry> registries)
        {
            foreach (var registry in registries)
            {
                registry.Register();
            }
        }
    }
}
