using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBLL _orderBLL;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderBLL orderBLL, ILogger<OrderController> logger)
        {
            _orderBLL = orderBLL;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<OrderModel> GetAllOrders()
        {
            return _orderBLL.GetAllOrders();
        }

        [HttpGet]
        [Route("getOrderByCustomer")]
        public IEnumerable<OrderModel> GetAllOrders(Guid customerId)
        {
            return _orderBLL.GetAllOrders(customerId);
        }

        [HttpGet]
        [Route("getOrder")]
        public OrderModel GetOrder(int orderId)
        {
            return _orderBLL.GetOrder(orderId);
        }

        [HttpPost]
        public ActionResult<OrderModel> AddOrder([FromBody] OrderCreationModel order)
        {
            Console.WriteLine(order);
            try
            {
                _logger.LogWarning("New order created");
                return _orderBLL.AddOrder(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError($"Failed to add order : {ex}");
                return BadRequest("Failed to add order");
            }
            
        }

    }
}
