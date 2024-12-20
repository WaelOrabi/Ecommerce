
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Extensions
{
    public static  class Registration
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)=> services.AddScoped<IUnitOfWork,UnitOfWork>();
        
    }
}
