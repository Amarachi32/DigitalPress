using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PressCore.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressInfrastructure.Extension
{
    public static class DBConnection
    {
        public static void AddDatabaseConnection(this IServiceCollection services)
        {
            IConfiguration config;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                config = serviceProvider.GetService<IConfiguration>();
            }
            string cc = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<PressContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });
        }

    }
}
