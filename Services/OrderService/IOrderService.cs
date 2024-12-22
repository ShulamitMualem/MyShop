using Entity;

namespace Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order newOrder);
        Task<Order> GetOrderById(int id);
    }
}