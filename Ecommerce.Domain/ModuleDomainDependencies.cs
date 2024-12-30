using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Domain
{
    public static class ModuleDomainDependencies
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
