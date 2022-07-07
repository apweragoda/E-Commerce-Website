using Business_Logic_Layer.Models;
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
        UserModel UpdateUser(Guid id, UserCreationModel user);
        //LoginModel LoginUserWithEmail(LogingCreationModel loging);
    }
}