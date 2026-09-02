using ONLINE_SHOPPING_API.Models;

namespace ONLINE_SHOPPING_API.Services
{
    public interface IProductService
    {
        Task<int> AddProduct(Product product);
        Task<Product?> GetProduct(int prdId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int prdId);
    }
}
