using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task Add(ProductDetails product);

        Task<ProductDetails> GetById(int id);

        void Update(ProductDetails product);
        Task<IEnumerable<ProductDetails>> SearchProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate);
    }
}
