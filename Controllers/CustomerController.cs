using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONLINE_SHOPPING_API.DTOs;
using ONLINE_SHOPPING_API.Services;

namespace ONLINE_SHOPPING_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly JwtService _jwtService;

        public CustomerController(
            ICustomerService customerService,
            JwtService jwtService)
        {
            _customerService = customerService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( CustomerRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerService.Register(dto);

            if (result == 0)
                return Conflict("Email already exists.");

            return Ok(new
            {
                message = "Customer registered successfully",
                customerId = result
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _customerService.Login(dto);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            var token = _jwtService.GenerateToken(
                user.UserId,
                user.UserName,
                user.Role);

            return Ok(new
            {
                message = "Login successful",
                token = token,
                userId = user.UserId,
                role = user.Role
            });
        }

        [HttpGet("{custId}")]
        [Authorize]

        public async Task<IActionResult> GetCustomer(int custId)
        {
            var customer =
                await _customerService.GetCustomer(custId);

            if (customer == null)
                return NotFound("Customer not found.");

            return Ok(customer);
        }

        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GetAllCustomers()
        {
            var customers =
                await _customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpPut("{custId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateCustomer(int custId, CustomerRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =  await _customerService.UpdateCustomer(custId, dto);

            if (!result)
                return NotFound("Customer not found.");

            return Ok("Profile updated successfully.");
        }

        [HttpPut("change-password")]
        [Authorize]

        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result =
                await _customerService.ChangePassword(dto.CustId, dto);

            if (!result)
                return BadRequest(
                    "Invalid old password or customer not found.");

            return Ok("Password changed successfully.");
        }

        [HttpDelete("{custId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCustomer(int custId)
        {
            var result =
                await _customerService.DeleteCustomer(custId);

            if (!result)
                return NotFound("Customer not found.");

            return Ok("Customer deleted successfully.");
        }
    }
}