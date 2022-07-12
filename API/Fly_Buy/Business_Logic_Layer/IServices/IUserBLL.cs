using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;

namespace Business_Logic_Layer
{
    public interface IUserBLL
    {
        UserModel AddUser(UserCreationModel user);
        int DeleteUser(Guid id);
        ICollection<UserModel> GetAllUsers();
        UserModel GetUser(Guid id);
        UserModel Login(LoginModel loginModel);
        UserModel UpdateUser(Guid id, UserCreationModel user);
        UserModel GetUserWithEmail(string email);
        UserModel RefreshExpiredJWTtoken(string refreshTokenToRenew);
        string GenerateToken(UserModel userModel);
        RefreshToken CreateRefreshToken();
        UserModel GenerateUserWithRefreshToken(UserModel user);
    }
}