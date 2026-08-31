using Dapper;
using Microsoft.Data.SqlClient;
using ONLINE_SHOPPING_API.Models;
using System.Data;

namespace ONLINE_SHOPPING_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> AddProduct(Product product)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "INSERT");
            param.Add("@prdName", product.PrdName);
            param.Add("@prdDescription", product.PrdDescription);
            param.Add("@prdPrice", product.PrdPrice);
            param.Add("@prdQuantity", product.PrdQuantity);
            param.Add("@prdImage", product.PrdImage);

            return await connection.ExecuteAsync(
                "sp_Product",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Product?> GetProduct(int prdId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GET");
            param.Add("@prdId", prdId);

            return await connection.QueryFirstOrDefaultAsync<Product>(
                "sp_Product",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GETALL");

            return await connection.QueryAsync<Product>(
                "sp_Product",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "UPDATE");
            param.Add("@prdId", product.PrdId);
            param.Add("@prdName", product.PrdName);
            param.Add("@prdDescription", product.PrdDescription);
            param.Add("@prdPrice", product.PrdPrice);
            param.Add("@prdQuantity", product.PrdQuantity);
            param.Add("@prdImage", product.PrdImage);

            var rows = await connection.ExecuteAsync(
                "sp_Product",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<bool> DeleteProduct(int prdId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "DELETE");
            param.Add("@prdId", prdId);

            var rows = await connection.ExecuteAsync(
                "sp_Product",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }
    }
}