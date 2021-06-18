using KC.WebApi.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KC.WebApi.Registry
{
    public class AutoMapperRegistry : IRegistry
    {
        private readonly IServiceCollection _services;

        public AutoMapperRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Register()
        {
            var types = new Type[]
            {
                typeof(ApiResourceToTransientMapper),
                typeof(DomainModelToApiResource)
            };

            _services.AddAutoMapper(types);
        }
    }
}
