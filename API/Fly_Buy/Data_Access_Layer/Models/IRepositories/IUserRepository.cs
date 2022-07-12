using Data_Access_Layer.Entities;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Repository
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User UpdateUser(User user);
        int DeleteUser(Guid id);
        ICollection<User> GetAllUsers();
        User GetUser(Guid id);
        User GetUserWithEmail(string email);
        void AddRefreshToken(User user, RefreshToken refreshToken);
        User GetUserByRefreshToken(string refreshToken);
    }  
}