﻿using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.NewFolder
{
    public class OrderRepository : IOrderRepository
    {
        MyShop328264650Context _dbcontext;
        public OrderRepository(MyShop328264650Context context)
        {
            _dbcontext = context;
        }
       
        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _dbcontext.Orders.Include(currentOrder=>currentOrder.User).FirstOrDefaultAsync(currentOrder => currentOrder.OrderId== id);
            return order == null ? null : order;// return order, if its null will return null.
        }
        public async Task<Order> CreateOrder(Order newOrder)
        {
            newOrder.OrserDate = DateOnly.FromDateTime(DateTime.Now);
            await _dbcontext.Orders.AddAsync(newOrder);
            
            await _dbcontext.SaveChangesAsync();
            Order order = await _dbcontext.Orders.Include(currentOrder => currentOrder.User).FirstOrDefaultAsync(currentOrder => currentOrder.OrderId == newOrder.OrderId);
            return order;
        }

    }
}
