using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders(Guid customerId);
        Order GetOrder(int orderId);
        IEnumerable<Order> GetAllOrders();
        Order AddOrder(Order order);
    }
}