
using DigitalPress.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using PressCore;
using PressCore.DBContext;
using PressCore.Interfaces;
using PressInfrastructure.Data;
using PressInfrastructure.Extension;

namespace DigitalPress
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDatabaseConnection();
            builder.Services.RegisterService();
         
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("errors/{0}");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            using  var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<PressContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();
                await PressContextSeed.SeedAsync(context);
            }catch (Exception ex) {
                logger.LogError(ex, "an error occur during migration");
            
            }

            await app.RunAsync();
        }
    }
}