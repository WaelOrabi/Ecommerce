using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Extensions
{
    public static  class Registration
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)=> services.AddScoped<IUnitOfWork,UnitOfWork>();
        
    }
}
