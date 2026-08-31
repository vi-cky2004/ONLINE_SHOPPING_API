using ONLINE_SHOPPING_API.Models;

namespace ONLINE_SHOPPING_API.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(int custId);

        Task<bool> AddOrderDetail(
            int orderId,
            int prdId,
            int quantity);

        Task<IEnumerable<dynamic>> GetOrder(int orderId);

        Task<IEnumerable<dynamic>> GetAllOrders();

        Task<IEnumerable<dynamic>> GetCustomerOrders(int custId);

        Task<bool> UpdateOrderStatus(
            int orderId,
            string status);
    }
}