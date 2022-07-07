using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Access_Layer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FlyBuyDbContext _ctx;

        public OrderRepository(FlyBuyDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Shipping)
                .Include(o => o.Bill)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Order> GetAllOrders(Guid customerId)
        {
            return _ctx.Orders
                .Include(o => o.Shipping)
                .Include(o => o.Bill)
                .Include(o => o.Items).ThenInclude(item => item.Product)
                .Where(o => o.CustomerId == customerId).ToList(); 
        }


        public Order GetOrder(int orderId)
        {
            return _ctx.Orders
                .Include(o => o.Shipping)
                .Include(o => o.Bill)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public Order AddOrder(Order order)
        {
            _ctx.Orders.Add(order);
            _ctx.SaveChanges();
            var createdOrder = _ctx.Orders
                .Include(o => o.Bill)
                .Include(o => o.Shipping)
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == order.Id);
            return createdOrder;
        }


    }
}


