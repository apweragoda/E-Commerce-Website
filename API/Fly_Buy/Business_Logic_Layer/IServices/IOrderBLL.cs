using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;

namespace Business_Logic_Layer.Services
{
    public interface IOrderBLL
    {
        IEnumerable<OrderModel> GetAllOrders();
        IEnumerable<OrderModel> GetAllOrders(Guid customerId);
        OrderModel GetOrder(int orderId);
        OrderModel AddOrder(OrderCreationModel order);
    }
}