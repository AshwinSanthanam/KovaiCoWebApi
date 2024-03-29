﻿using KC.WebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KC.WebApi.Registry
{
    public class ServiceRegistry : IRegistry
    {
        private readonly IServiceCollection _services;

        public ServiceRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Register()
        {
            _services.AddScoped<IJwtService, JwtService>();
            _services.AddScoped<IExternalTokenValidationService, ExternalTokenValidationService>();
            _services.AddScoped<IUserService, UserService>();
            _services.AddScoped<IProductService, ProductService>();
            _services.AddScoped<ICartService, CartService>();
        }
    }
}
