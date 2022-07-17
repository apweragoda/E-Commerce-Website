using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Data_Access_Layer.Repository;
using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Data_Access_Layer.Entities;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Business_Logic_Layer
{
    public class UserBLL : IUserBLL
    {
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        private readonly IMapper userMapper;
        private readonly ILogger logger;


        public UserBLL()
        {

        }
        public UserBLL(IOptions<AppSettings> appSettings, IUserRepository DAL, IMapper mapper, ILogger<UserBLL> logger)
        {
            this.appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            this.userRepository = DAL;
            this.userMapper = mapper;
            this.logger = logger;
        }

        public UserModel Login(LoginModel loginModel)
        {
            var customer = userRepository.GetUserWithEmail(loginModel.Email);

            if (customer == null)
            {
                return null;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, customer.Password);

            if (!isValidPassword)
            {
                logger.LogWarning("Password incorrect");
                return null;
            }
            var result = userMapper.Map<UserModel>(customer);

            var userModel = GenerateUserWithRefreshToken(result);

            return userModel;
        }

        public ICollection<UserModel> GetAllUsers()
        {
            var user = userRepository.GetAllUsers();
            var result = userMapper.Map<ICollection<UserModel>>(user);
            logger.LogWarning("Displaying all user details");
            return result;
        }

        public UserModel GetUser(Guid id)
        {
            var user = userRepository.GetUser(id);
            var result = userMapper.Map<UserModel>(user);
            logger.LogWarning("User: " + user.UserName + " searched");
            return result;
        }

        public UserModel AddUser(UserCreationModel userCreationModel)
        {
            var userExist = userRepository.GetUserWithEmail(userCreationModel.Email);
            if (userExist != null)
            {
                return null;
            }
            userCreationModel.UserId = Guid.NewGuid();
            var userEntity = new User()
            {
                UserId = userCreationModel.UserId,
                UserName = userCreationModel.UserName,
                Email = userCreationModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userCreationModel.Password)
            };

            var changes = userRepository.AddUser(userEntity);

            var result = userMapper.Map<UserModel>(changes);

            var newCustomerDto = GenerateUserWithRefreshToken(result); 
            logger.LogWarning("User: " + userEntity.UserName + " added");
            return newCustomerDto;
        }

        public UserModel UpdateUser(Guid id,UserCreationModel user)
        {
            var existingUser = userRepository.GetUser(id);
            if(existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                var userEntity = userMapper.Map<User>(existingUser);
                var changes = userRepository.UpdateUser(userEntity);
                var userModel = userMapper.Map<UserModel>(changes);
                logger.LogWarning("User: " + user.UserName + " updated");
                return userModel;
            }
            logger.LogWarning("User not found");
            return null;
            
        }

        public int DeleteUser(Guid id)
        {
            var changes = userRepository.DeleteUser(id);
            logger.LogWarning("User: " + id + " deleted");
            return changes;
        }

        public UserModel GetUserWithEmail(string email)
        {
            logger.LogWarning("Get user by email");
            var verified = userRepository.GetUserWithEmail(email);
            if(verified == null)
            {
                logger.LogError("Invalid input");
                return null;
            }
            var userModel = userMapper.Map<UserModel>(verified);
            logger.LogWarning("Getting user details by email - " + userModel.UserName);
            return userModel;
        }
        public UserModel GenerateUserWithRefreshToken(UserModel user)
        {
            var userModel = new UserModel
            {
                UserId = user.UserId,
                IsAuthenticated = true,
                Token = GenerateToken(user),
                Email = user.Email,
                UserName = user.UserName
            };

            var refreshToken = CreateRefreshToken();
            userModel.RefreshToken = refreshToken.Token;
            userModel.RefreshTokenExpiration = refreshToken.Expires;

            var model = userMapper.Map<User>(userModel);
            userRepository.AddRefreshToken(model, refreshToken);

            return userModel;
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }


        public string GenerateToken(UserModel userModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(appSettings.JWTkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userModel.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserModel RefreshExpiredJWTtoken(string refreshTokenToRenew)
        {
            var userModel = new UserModel();

            // get user that have the refresh token
            var user = userRepository.GetUserByRefreshToken(refreshTokenToRenew);

            if (user == null)
            {
                userModel.IsAuthenticated = false;
                userModel.Message = "No user with the token found";
                return userModel;
            }

            // get the refreshtoken details 
            var refreshToken = user.RefreshTokens.Single(r => r.Token == refreshTokenToRenew);

            // if refresh token is not active (revoked && expired)
            if (!refreshToken.IsActive)
            {
                userModel.IsAuthenticated = false;
                userModel.Message = "Token is not active";
                return userModel;
            }

            // revoking the current refresh token
            refreshToken.Revoked = DateTime.Now;

            // creating new refresh token
            var newRefreshToken = CreateRefreshToken();
            userRepository.AddRefreshToken(user, newRefreshToken);
            var model = userMapper.Map<UserModel>(user);
            userModel.IsAuthenticated = true;
            userModel.Token = GenerateToken(model);
            userModel.Email = user.Email;
            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;
            userModel.RefreshToken = newRefreshToken.Token;
            userModel.RefreshTokenExpiration = refreshToken.Expires;
            return userModel;
        }
    }
}
