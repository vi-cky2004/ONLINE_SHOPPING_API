using Dapper;
using Microsoft.Data.SqlClient;
using ONLINE_SHOPPING_API.Models;
using System.Data;

namespace ONLINE_SHOPPING_API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> RegisterCustomer(Customer customer)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "INSERT");
            param.Add("@custName", customer.CustName);
            param.Add("@custAddress", customer.CustAddress);
            param.Add("@custPhone", customer.CustPhone);
            param.Add("@custEmail", customer.CustEmail);
            param.Add("@custpassword", customer.CustPassword);

            return await connection.ExecuteAsync( "sp_Customer", param,commandType: CommandType.StoredProcedure);
        }

        public async Task<Customer?> LoginCustomer(string email, string password)
        {
            var customer = await GetCustomerByEmail(email);

            if (customer == null)
                return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify( password, customer.CustPassword);

            if (!validPassword)
                return null;

            return customer;
        }

        public async Task<Customer?> GetCustomerById(int custId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GET");
            param.Add("@custId", custId);

            return await connection.QueryFirstOrDefaultAsync<Customer>("sp_Customer",   param,   commandType: CommandType.StoredProcedure);
        }
        public async Task<Customer?> GetCustomerByEmail(string email)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GETBYEMAIL");
            param.Add("@custEmail", email);

            return await connection.QueryFirstOrDefaultAsync<Customer>(  "sp_Customer", param, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "GETALL");

            return await connection.QueryAsync<Customer>(
                "sp_Customer",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "UPDATE");
            param.Add("@custId", customer.CustId);
            param.Add("@custName", customer.CustName);
            param.Add("@custAddress", customer.CustAddress);
            param.Add("@custPhone", customer.CustPhone);
            param.Add("@custEmail", customer.CustEmail);

            var rows = await connection.ExecuteAsync(
                "sp_Customer",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<bool> ChangePassword(
      int custId,
      string newPassword)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "CHANGEPASSWORD");
            param.Add("@custId", custId);
            param.Add("@custpassword", newPassword);

            var rows = await connection.ExecuteAsync(
                "sp_Customer",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }
        public async Task<bool> DeleteCustomer(int custId)
        {
            using var connection = CreateConnection();

            var param = new DynamicParameters();

            param.Add("@action", "DELETE");
            param.Add("@custId", custId);

            var rows = await connection.ExecuteAsync(
                "sp_Customer",
                param,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }
    }
}