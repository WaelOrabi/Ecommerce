using Application.Interfaces;
using Application.Services;
using Application.Services.Interfaces;
using Ecommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class Registeration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) { 
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
