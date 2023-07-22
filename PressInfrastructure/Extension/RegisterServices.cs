using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PressCore.Interfaces;
using PressInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressInfrastructure.Extension
{
    public static class RegisterServices
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IPressRepository<>), typeof(PressRepository<>));
            //services.AddTransient<IUnitOfWork, UnitOfWork<HmoDbContext>>();
            //services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
