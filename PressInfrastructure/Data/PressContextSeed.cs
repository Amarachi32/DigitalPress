using Microsoft.Extensions.Logging;
using PressCore.DBContext;
using PressCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PressInfrastructure.Data
{
    public class PressContextSeed
    {
        public static async Task SeedAsync(PressContext context)
        {
           
            if (!context.ProductBrands.Any())
            {
                
                try
                {
                    var brandsData = File.ReadAllText("../PressInfrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    context.ProductBrands.AddRange(brands);
                    // Rest of the code
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, display error, etc.)
                    //"an error occur during migration";
                     
                }
               // var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
              
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../PressInfrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../PressInfrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
