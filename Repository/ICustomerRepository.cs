using ONLINE_SHOPPING_API.Models;

namespace ONLINE_SHOPPING_API.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> RegisterCustomer(Customer customer);

        Task<LoginUser?> LoginCustomer(string email);

        Task<Customer?> GetCustomerById(int custId);
        Task<Customer?> GetCustomerByEmail(string custEmail);

        Task<IEnumerable<Customer>> GetAllCustomers();

        Task<bool> UpdateCustomer(Customer customer);

        Task<bool> ChangePassword(int custId, string newPassword);

        Task<bool> DeleteCustomer(int custId);
    }
}