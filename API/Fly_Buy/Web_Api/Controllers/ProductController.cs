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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBLL productBLL;
        private readonly ILogger logger;


        public ProductController(IProductBLL productBLL, ILogger<ProductController> logger)
        {
            this.productBLL = productBLL;
            this.logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(productBLL.GetAllProducts());
        }

        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetSingleProduct")]
        public IActionResult GetSingleProduct(int id)
        {
            try
            {
                var product = productBLL.GetSingleProduct(id);
                if (product != null)
                {
                    logger.LogWarning("Getting single product");
                    return Ok(product);
                }
                return NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to getting product - {ex} ");
            }
        }

        // GET api/<ProductController>/5
        [HttpGet]
        [Route("{category}")]
        [ActionName("GetProductsByCategory")]
        public IActionResult GetProductsByCategory(string category)
        {
            return Ok(productBLL.GetProductsByCategory(category));
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductCreationModel productModel)
        {
            try
            {
                var product = productBLL.AddProduct(productModel);
                if(product != null)
                    return Ok(product);

                return BadRequest("Invalid input!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add product - {ex} ");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public ActionResult UpdateProduct([FromBody] ProductCreationModel productModel)
        {
            try
            {
                var product = productBLL.UpdateProduct(productModel);
                if(product != null)
                    return Ok(product);

                return BadRequest("Invalid input");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update product - {ex} ");
            }
        }
       

        // DELETE api/<ProductController>/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = productBLL.DeleteProduct(id);
                if(result > 0)
                    return StatusCode(201, "Product added successfully");
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete product - {ex} ");
            }
        }
    }
}
