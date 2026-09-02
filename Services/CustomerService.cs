using BCrypt.Net;
using ONLINE_SHOPPING_API.DTOs;
using ONLINE_SHOPPING_API.Models;
using ONLINE_SHOPPING_API.Repositories;

namespace ONLINE_SHOPPING_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Register(CustomerRegisterDto dto)
        {
          
            var existingCustomer =
                await _customerRepository.GetCustomerByEmail(dto.CustEmail);

            if (existingCustomer != null)
                return 0;

           
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.CustPassword);

            var customer = new Customer
            {
                CustName = dto.CustName,
                CustAddress = dto.CustAddress,
                CustPhone = dto.CustPhone,
                CustEmail = dto.CustEmail,
                CustPassword = hashedPassword
            };

            return await _customerRepository.RegisterCustomer(customer);
        }

        public async Task<Customer?> Login(LoginDto dto)
        {

            return  await _customerRepository.LoginCustomer(dto.Email,dto.Password);
        }

        public async Task<Customer?> GetCustomer(int custId)
        {
            return await _customerRepository.GetCustomerById(custId);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        public async Task<bool> UpdateCustomer(
            int custId,
            CustomerRegisterDto dto)
        {
            var customer =
                await _customerRepository.GetCustomerById(custId);

            if (customer == null)
                return false;

            customer.CustId = custId;
            customer.CustName = dto.CustName;
            customer.CustAddress = dto.CustAddress;
            customer.CustPhone = dto.CustPhone;
            customer.CustEmail = dto.CustEmail;

            return await _customerRepository.UpdateCustomer(customer);
        }

        public async Task<bool> ChangePassword(
            int custId,
            ChangePasswordDto dto)
        {
            var customer =
                await _customerRepository.GetCustomerById(custId);

            if (customer == null)
                return false;

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.OldPassword,
                    customer.CustPassword);

            if (!validPassword)
                return false;

            string newHash =
                BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            return await _customerRepository.ChangePassword(
                custId,
                newHash);
        }

        public async Task<bool> DeleteCustomer(int custId)
        {
            return await _customerRepository.DeleteCustomer(custId);
        }
    }
}