using Microsoft.EntityFrameworkCore;
using PressCore.DBContext;
using PressCore.Entities;
using PressCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressInfrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly PressContext _context;

        public ProductRepository(PressContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }


        public async Task<Product> GetProductByIdAsync(string id)
        {
            var product = await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));

            return product;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
