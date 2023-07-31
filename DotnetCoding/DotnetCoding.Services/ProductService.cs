using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAll();
            return products.Where(p => p.ProductStatus == "Active")
                           .OrderByDescending(p => p.PostedDate);
        }

        public async Task<ProductDetails> CreateProduct(ProductDetails product)
        {
            // Check if the product price exceeds the limit
            if (product.ProductPrice > 10000)
            {
                product.ProductStatus = "PendingApproval";
            }
            else
            {
                product.ProductStatus = "Active";
            }

            _unitOfWork.Products.Add(product);
            _unitOfWork.Save();

            return product;
        }

        public async Task<ProductDetails> UpdateProduct(ProductDetails product)
        {
            var existingProduct = await _unitOfWork.Products.GetById(product.Id);

            if (existingProduct == null)
                return null;

            // Check if the product price exceeds the limit or is more than 50% of the previous price
            if (product.ProductPrice > 5000 || product.ProductPrice > existingProduct.ProductPrice * 1.5m)
            {
                product.ProductStatus = "PendingApproval";
            }
            else
            {
                product.ProductStatus = "Active";
            }

            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();

            return product;
        }



        public async Task<bool> DeleteProduct(int productId)
        {
            var existingProduct = await _unitOfWork.Products.GetById(productId);

            if (existingProduct == null)
                return false;

            existingProduct.ProductStatus = "PendingApproval";
            _unitOfWork.Products.Update(existingProduct);
            _unitOfWork.Save();

            return true;
        }

        public async Task<IEnumerable<ProductDetails>> GetProductsInApprovalQueue()
        {
            var products = await _unitOfWork.Products.GetAll();
            return products.Where(p => p.ProductStatus == "PendingApproval").OrderByDescending(p => p.PostedDate);
        }

    }
}
