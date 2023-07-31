using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();
        Task<ProductDetails> CreateProduct(ProductDetails product);
        Task<ProductDetails> UpdateProduct(ProductDetails product);
        Task<bool> DeleteProduct(int productId);
        Task<IEnumerable<ProductDetails>> GetProductsInApprovalQueue();
    }
}
