using AutoMapper;
using Business_Logic_Layer;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBLL BLL;
        private readonly ILogger _logger;


        public ProductController(IProductBLL BLL, ILogger<ProductController> logger )
        {
            this.BLL = BLL;
            _logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<ProductModel> GetAllProducts()
        {
            //return repository.GetAllProducts();
            return BLL.GetAllProducts();
            //_logger.LogInformation("Displaying all the products");
        }

        // GET api/<ProductController>/5
        [HttpGet("{category}")]
        public IEnumerable<ProductModel> GetProductsByCategory(string category)
        {
            return BLL.GetAllProducts();
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("add")]
        public ActionResult AddProduct([FromBody] ProductCreationModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BLL.AddProduct(productModel);
                    return Ok();
                    //return BadRequest($"Invalid input!");
                }
                return BadRequest($"Invalid Failed to add product ");

            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add product - {ex} ");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        [Route("update")]
        public ActionResult UpdateProduct([FromBody] ProductCreationModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BLL.UpdateProduct(productModel);
                    return Ok();
                    //return BadRequest($"Invalid input!");
                }
                return BadRequest($"Invalid input");

            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update product - {ex} ");
            }
        }
       

        // DELETE api/<ProductController>/5
        [HttpDelete]
        [Route("delete")]

        public ActionResult Delete(int id)
        {
            try
            {
                BLL.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete product - {ex} ");
            }
        }
    }
}
