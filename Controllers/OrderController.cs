using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONLINE_SHOPPING_API.Services;

namespace ONLINE_SHOPPING_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(int custId)
        {
            var orderId = await _orderService.CreateOrder(custId);

            return Ok(new
            {
                message = "Order created successfully",
                orderId = orderId
            });
        }


        [Authorize(Roles = "Customer")]
        [HttpPost("{orderId}/details")]
        public async Task<IActionResult> AddOrderDetail(
            int orderId,
            int prdId,
            int quantity)
        {
            var result = await _orderService.AddOrderDetail(
                orderId,
                prdId,
                quantity);

            if (!result)
                return BadRequest("Unable to add product to order.");

            return Ok("Product added to order successfully.");
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);

            if (order == null || !order.Any())
                return NotFound("Order not found.");

            return Ok(order);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();

            return Ok(orders);
        }



        [Authorize(Roles = "Customer")]
        [HttpGet("customer/{custId}")]
        public async Task<IActionResult> GetCustomerOrders(int custId)
        {
            var orders =  await _orderService.GetCustomerOrders(custId);

            return Ok(orders);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(
            int orderId,
            string status)
        {
            var result =
                await _orderService.UpdateOrderStatus(
                    orderId,
                    status);

            if (!result)
                return NotFound("Order not found.");

            return Ok("Order status updated successfully.");
        }
    }
}