using Application.Services;
using Application.Services.Interfaces;
using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace Application.Extensions
{
    public static class Registeration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services) { 
            
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IOrderService, OrderService>();
            return services;
        }
        public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services) {
            services.AddSwaggerGen(options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name="JWT Authentication",
                    Description="Enter your JWT token in this field",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.Http,
                    Scheme=JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat="JWT"
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    },[]}
                };
                options.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}
