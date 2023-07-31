using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDetails product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            if (createdProduct == null)
            {
                return BadRequest("Product creation failed.");
            }

            return Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDetails product)
        {
            if (id != product.Id)
            {
                return BadRequest("Invalid product data.");
            }

            var updatedProduct = await _productService.UpdateProduct(product);
            if (updatedProduct == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var isDeleted = await _productService.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound("Product not found.");
            }

            return Ok("Product deleted successfully.");
        }

        [HttpGet("approval-queue")]
        public async Task<IActionResult> GetProductsInApprovalQueue()
        {
            var approvalQueueProducts = await _productService.GetProductsInApprovalQueue();
            return Ok(approvalQueueProducts);
        }
    }
}
