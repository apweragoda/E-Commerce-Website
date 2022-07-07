using AutoMapper;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Services
{
    public class OrderBLL : IOrderBLL
    {
        private readonly IMailService _mailService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _orderMapper;

        public OrderBLL(IMailService mailService, IOrderRepository repository, IUserRepository userRepository, IMapper mapper)
        {
            _mailService = mailService;
            _userRepository = userRepository;
            _orderRepository = repository;
            _orderMapper = mapper;
        }

        public IEnumerable<OrderModel> GetAllOrders(Guid customerId)
        {
            var orders = _orderRepository.GetAllOrders(customerId);
            var result = _orderMapper.Map <IEnumerable<OrderModel>>(orders) ;
            return result;
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            var result = _orderMapper.Map<IEnumerable<OrderModel>>(orders);
            return result;
        }

        public OrderModel GetOrder(int orderId)
        {
            var orderEntity = _orderRepository.GetOrder(orderId);

            if(orderEntity == null)
            {
                return null;
            }

            var orderModel = new OrderModel()
            {
                Id = orderEntity.Id,
                CustomerId = _orderMapper.Map <UserModel>(orderEntity.CustomerId),
                OrderDate = orderEntity.OrderDate,
                Items = _orderMapper.Map<ICollection<OrderItemModel>>(orderEntity.Items),
                Shipping = _orderMapper.Map<ShippingModel>(orderEntity.Shipping),
                Bill = _orderMapper.Map<BillModel>(orderEntity.Bill)
            };

            return orderModel;
        }

        public OrderModel AddOrder(OrderCreationModel order)
        {
            var orderDetails = _orderMapper.Map<Order>(order);
            var createdOrder = _orderRepository.AddOrder(orderDetails);
            var createdOrderModel = _orderMapper.Map<OrderModel>(createdOrder);

            //send email when order palced
            var eto = _userRepository.GetUser(order.CustomerId);
            Console.WriteLine(order.ToString());
            _mailService.SendMessage(eto.Email, "Order Placed!", order.ToString());

            return createdOrderModel;
        }
    }
}
