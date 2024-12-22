using Entity;
using Repository.NewFolder;
using Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }
        public async Task<Order> CreateOrder(Order newOrder)
        {
            return await _orderRepository.CreateOrder(newOrder);
        }
    }
}
