using Business_Logic_Layer.Models;
using Data_Access_Layer;
using System;
using AutoMapper;
using System.Collections.Generic;
using Data_Access_Layer.Repository.Entities;
using System.Linq;
using Data_Access_Layer.Repository;
using Microsoft.Extensions.Logging;

namespace Business_Logic_Layer
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper userMapper;
        private readonly ILogger logger;

        //private readonly UserDAL _userRepository;

        public UserBLL()
        {

        }
        public UserBLL(IUserRepository DAL, IMapper mapper, ILogger<UserBLL> logger)
        {
            this.userRepository = DAL;
            this.userMapper = mapper;
            this.logger = logger;
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
            logger.LogWarning("User: " + user.FirstName + " searched");
            return result;
        }

        public UserModel AddUser(UserCreationModel user)
        {
            user.Id = Guid.NewGuid();
            var userEntity = userMapper.Map<User>(user);
            var changes = userRepository.AddUser(userEntity);
            var userModel = userMapper.Map<UserModel>(changes);
            logger.LogWarning("User: " + user.FirstName + " added");
            return userModel;
        }

        public UserModel UpdateUser(Guid id,UserCreationModel user)
        {
            var existingUser = userRepository.GetUser(id);
            if(existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                var userEntity = userMapper.Map<User>(existingUser);
                var changes = userRepository.UpdateUser(userEntity);
                var userModel = userMapper.Map<UserModel>(changes);
                logger.LogWarning("User: " + user.FirstName + " updated");
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

        /*
        public LoginModel LoginUserWithEmail(LogingCreationModel loging)
        {
            _logger.LogWarning("User trying to login");
            var verified = _userRepository.LoginUserWithEmail(loging.Email, loging.Password);
            if(verified == null)
            {
                _logger.LogError("Invalid credentials");
                return null;
            }
            var userModel = _userMapper.Map<LoginModel>(verified);
            _logger.LogWarning("User logged in - " + userModel.UserName);
            return userModel;
        }
       */
    }
}
