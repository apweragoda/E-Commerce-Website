using Business_Logic_Layer;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Repository.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]

        public IActionResult Login(LoginModel login)
        {
            logger.LogInformation("Trying to login");
            var customer = userBLL.Login(login);

            if (customer == null)
            {
                logger.LogError("Invalid username or password");
                return Unauthorized("Incorrect inputs !");
            }
            
            SetRefreshTokenInCookie(customer.RefreshToken);

            logger.LogInformation($"{customer.UserName} logged in - {customer}");
            return Ok(customer);
        }


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

        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public ActionResult AddUser([FromBody] UserCreationModel userModel)
        {
            logger.LogWarning("New user registration requested");
            var newUser = userBLL.AddUser(userModel);

            if (newUser == null)
            {
                logger.LogError("Email already in use");
                return BadRequest(new { message = "Email address already in use" });
            }

            SetRefreshTokenInCookie(newUser.RefreshToken);

            logger.LogWarning("New customer registered");

            return Ok(new { message = "Registered Successfully !", user = newUser });
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

        [AllowAnonymous]
        [HttpGet]
        [Route("refreshToken")]
        public ActionResult RefreshJWTtoken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var userModel = userBLL.RefreshExpiredJWTtoken(refreshToken);
            if (!string.IsNullOrEmpty(userModel.RefreshToken))
            {
                SetRefreshTokenInCookie(userModel.RefreshToken);
            }
            return Ok(userModel);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpGet]
            public string SayHello()
            {
                return "Working";
            }
        }
    }

