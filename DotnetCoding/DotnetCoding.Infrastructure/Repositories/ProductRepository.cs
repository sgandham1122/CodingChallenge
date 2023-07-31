using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {

        }


        public void Update(ProductDetails product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task<ProductDetails> GetById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task Add(ProductDetails product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDetails>> SearchProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate)
        {
            var query = _dbContext.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p => p.ProductName.Contains(searchTerm));

            if (minPrice.HasValue)
                query = query.Where(p => p.ProductPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);

            if (minPostedDate.HasValue)
                query = query.Where(p => p.PostedDate >= minPostedDate.Value);

            if (maxPostedDate.HasValue)
                query = query.Where(p => p.PostedDate <= maxPostedDate.Value);

            return await query.OrderByDescending(p => p.PostedDate).ToListAsync();
        }
    }
}
