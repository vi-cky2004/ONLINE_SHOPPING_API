using ONLINE_SHOPPING_API.Repositories;

namespace ONLINE_SHOPPING_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> CreateOrder(int custId)
        {
            return await _orderRepository.CreateOrder(custId);
        }

        public async Task<bool> AddOrderDetail(
            int orderId,
            int prdId,
            int quantity)
        {
            return await _orderRepository.AddOrderDetail(
                orderId,
                prdId,
                quantity);
        }

        public async Task<IEnumerable<dynamic>> GetOrder(int orderId)
        {
            return await _orderRepository.GetOrder(orderId);
        }

        public async Task<IEnumerable<dynamic>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        public async Task<IEnumerable<dynamic>> GetCustomerOrders(
            int custId)
        {
            return await _orderRepository.GetCustomerOrders(custId);
        }

        public async Task<bool> UpdateOrderStatus(
            int orderId,
            string status)
        {
            return await _orderRepository.UpdateOrderStatus(
                orderId,
                status);
        }
    }
}