using Dapper;
using Microsoft.Data.SqlClient;
using ONLINE_SHOPPING_API.Models;
using System.Data;

namespace ONLINE_SHOPPING_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> CreateOrder(int custId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "CREATE");
            param.Add("@custId", custId);

            return await connection.ExecuteScalarAsync<int>(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> AddOrderDetail(
            int orderId,
            int prdId,
            int quantity)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "ADDDETAIL");
            param.Add("@orderId", orderId);
            param.Add("@prdId", prdId);
            param.Add("@orderQuantity", quantity);

            var rows = await connection.ExecuteAsync(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<IEnumerable<dynamic>> GetOrder(int orderId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GET");
            param.Add("@orderId", orderId);

            return await connection.QueryAsync(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> GetAllOrders()
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GETALL");

            return await connection.QueryAsync(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<dynamic>> GetCustomerOrders(
            int custId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GETBYCUSTOMER");
            param.Add("@custId", custId);

            return await connection.QueryAsync(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateOrderStatus(
            int orderId,
            string status)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "STATUS");
            param.Add("@orderId", orderId);
            param.Add("@ordStatus", status);

            var rows = await connection.ExecuteAsync(
                "sp_Order",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }
    }
}