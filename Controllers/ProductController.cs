using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONLINE_SHOPPING_API.Models;
using ONLINE_SHOPPING_API.Services;

namespace ONLINE_SHOPPING_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
        public class ProductController : ControllerBase
        {
            private readonly IProductService _productService;

            public ProductController(IProductService productService)
            {
                _productService = productService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllProducts()
            {
                var products = await _productService.GetAllProducts();

                return Ok(products);
            }

            [HttpGet("{prdId}")]
            public async Task<IActionResult> GetProduct(int prdId)
            {
                var product = await _productService.GetProduct(prdId);

                if (product == null)
                    return NotFound("Product not found");

                return Ok(product);
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            public async Task<IActionResult> AddProduct(Product product)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productService.AddProduct(product);

                return Ok(new
                {
                    message = "Product added successfully",
                    productId = result
                });
            }

            [Authorize(Roles = "Admin")]
            [HttpPut("{prdId}")]
            public async Task<IActionResult> UpdateProduct(int prdId, Product product)
            {
                if (!ModelState.IsValid)
                return BadRequest(ModelState);
                product.PrdId = prdId;

                var result = await _productService.UpdateProduct(product);

                if (!result)
                return NotFound("Product not found");

                return Ok("Product updated successfully");
            }

            [Authorize(Roles = "Admin")]
            [HttpDelete("{prdId}")]
            public async Task<IActionResult> DeleteProduct(int prdId)
            {
                var result = await _productService.DeleteProduct(prdId);

                if (!result)
                    return NotFound("Product not found");

                return Ok("Product deleted successfully");
            }
        }
    }

