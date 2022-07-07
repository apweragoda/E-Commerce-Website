using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[Controller]")]
    public class ShippingController : Controller
    {
        private readonly IShippingBLL _shippingBLL;
        private readonly ILogger<ShippingController> _logger;

        public ShippingController(IShippingBLL shippingBLL, ILogger<ShippingController> logger)
        {
            _shippingBLL = shippingBLL;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ShippingModel> GetAllShippingDetails()
        {
            return _shippingBLL.GetAllShippingDetails();
        }

        [HttpPost]
        public ActionResult<ShippingModel> AddShippingDetails([FromBody] ShippingCreationModel shipping)
        {
            try
            {
                _logger.LogWarning("New shipping details created");
                return _shippingBLL.AddShippingDetails(shipping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add shipping details : {ex}");
                return BadRequest("Failed to add shipping details");
            }

        }
    }
}
