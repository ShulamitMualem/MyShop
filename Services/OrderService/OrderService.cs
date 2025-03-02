using Entity;
using Microsoft.Extensions.Logging;
using Repository.NewFolder;
using Repository.ProductsRepo;
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
        IProductsRepository _productsRepository;
        ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IProductsRepository productsRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _logger = logger;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }
        public async Task<Order> CreateOrder(Order newOrder)
        {
            if(!await CheckSum(newOrder))
               {
                _logger.LogCritical("someone try to still your shop!!!!!!!!!!!");
                return null; 
            }

            return await _orderRepository.CreateOrder(newOrder);
        }
        private async Task<bool> CheckSum(Order order)
        {
            List<Product> products = await _productsRepository.Get(0,0,null,null,null, []);
            decimal amount= 0;
            foreach (var item in order.OrderItems)
            {
               amount+= products.Find(product=>product.ProductId==item.ProductId).Price;
            }
            return amount == order.OrderSum;
        }
    }
}
