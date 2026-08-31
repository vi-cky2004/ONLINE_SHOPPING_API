using ONLINE_SHOPPING_API.DTOs;
using ONLINE_SHOPPING_API.Models;

namespace ONLINE_SHOPPING_API.Services
{
    public interface ICustomerService
    {
        Task<int> Register(CustomerRegisterDto dto);

        Task<Customer?> Login(LoginDto dto);

        Task<Customer?> GetCustomer(int custId);

        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> UpdateCustomer(int custId, CustomerRegisterDto dto);

        Task<bool> ChangePassword(int custId, ChangePasswordDto dto);

        Task<bool> DeleteCustomer(int custId);
    }
}