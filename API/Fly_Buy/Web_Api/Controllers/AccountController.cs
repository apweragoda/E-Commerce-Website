using Business_Logic_Layer;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly IMailService mailService;
        private readonly IUserBLL userBLL;

        public AccountController(ILogger<AccountController> logger, IMailService mailService, IUserBLL BLL)
        {
            this.logger = logger;
            this.mailService = mailService;
            this.userBLL = BLL;
        }



        /*
         * 
         * public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                logger.LogWarning("User authenticated");
                return RedirectToAction("GetAllProducts", "Product");
            }
            logger.LogWarning("User need to log in");
            return BadRequest("User need to log in");
        }
         * 
         * 
         * 
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LogingCreationModel loging)
        {
            //_userBLL.LoginUserWithEmail(loging);
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(loging.UserName,
                    loging.PasswordHash,
                    loging.RememberMe,
                    false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        logger.LogWarning("User not logged in");
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        logger.LogWarning("User logged in");
                        return RedirectToAction("GetAllOrders", "Order");
                    }
                }
            }
            ModelState.AddModelError("", "Falied to login");
            logger.LogWarning("Failed to login user");
            return BadRequest($"Failed to login user");
        /*
            /*
            try
            {
                if (this.User.Identity.IsAuthenticated)
                {
                    _logger.LogWarning("User authenticated");
                    return Ok();
                }
                _userBLL.LoginUserWithEmail(loging);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to login user - {ex} ");
            }
            */


        [HttpGet]
        [Route("getUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(userBLL.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get users - {ex} ");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetUser")]
        public UserModel GetUser([FromRoute] Guid id)
        {
            return userBLL.GetUser(id);
        }

        [HttpPost]
        [Route("addUser")]
        public ActionResult AddUser([FromBody] UserCreationModel userModel)
        {
            try
            {
                userBLL.AddUser(userModel);
                logger.LogWarning("User added");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add user - {ex} ");
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult UpdateUser([FromRoute] Guid id,[FromBody] UserCreationModel userModel)
        {
            try
            {
                userBLL.UpdateUser(id, userModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update user - {ex} ");
            }            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public ActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                userBLL.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete user - {ex} ");
            }
        }


        [HttpGet]
            public string SayHello()
            {
                return "Working";
            }
        }
    }

