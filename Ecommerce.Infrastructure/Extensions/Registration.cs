using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Interfaces;
using Ecommerce.Infrastructure.RepositoriesImplementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Extensions
{
    public static class Registration
    {
        public static IServiceCollection RegisterationIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(option =>
            {
                // Password settings
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;

                // Sign-in settings
                option.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection RegisterationUnitOfWork(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
