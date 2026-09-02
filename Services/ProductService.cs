using ONLINE_SHOPPING_API.Models;
using ONLINE_SHOPPING_API.Repositories;

namespace ONLINE_SHOPPING_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product?> GetProduct(int prdId)
        {
            return await _productRepository.GetProduct(prdId);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var existingProduct =
                await _productRepository.GetProduct(product.PrdId);

            if (existingProduct == null)
                return false;

            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int prdId)
        {
            return await _productRepository.DeleteProduct(prdId);
        }
    }
}