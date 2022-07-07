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
    public class BillController : Controller
    {
        private readonly IBillBLL _billBLL;
        private readonly ILogger<BillController> _logger;

        public BillController(IBillBLL billBLL, ILogger<BillController> logger)
        {
            _billBLL = billBLL;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<BillModel> GetAllBillDetails()
        {
            return _billBLL.GetAllBillDetails();
        }

        [HttpPost]
        public ActionResult<BillModel> AddBill([FromBody] BillCreationModel bill)
        {
            try
            {
                _logger.LogWarning("New bill created");
                return _billBLL.AddBillDetails(bill);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add order : {ex}");
                return BadRequest("Failed to add order");
            }

        }
    }
}
