using Entity;

namespace Repository.NewFolder
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}