using Application.Services.Interfaces;
using Ecommerce.Application.Extensions;
using Ecommerce.Application.Services.Implementation;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Reflection;
using System.Text;
namespace Application.Extensions
{
    public static class Registeration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICartService, CartService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<TokenProvider>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"])),
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:Audience"],

                    };
                });
            return services;
        }
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {

                options.AddPolicy("Admins", builder =>
                {
                    builder.RequireRole("Admin");

                });
            });
            return services;
        }
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
        public static IServiceCollection RegisterValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
        public static IServiceCollection RegisterLocalization(this IServiceCollection services)
        {

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo> {
                       new CultureInfo("en-US"),
                       new CultureInfo("ar-EG")
                      };
                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            return services;
        }
    }
}
