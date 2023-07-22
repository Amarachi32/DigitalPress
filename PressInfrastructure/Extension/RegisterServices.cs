using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PressCore.Interfaces;
using PressInfrastructure.Data;
using PressInfrastructure.Errors;

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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                        var errorResponse = new ApiValidationErrorResponse
                        {
                            Errors = errors,
                        };
                        return new BadRequestObjectResult(errorResponse);
                    };

            });
        }
    }
}
